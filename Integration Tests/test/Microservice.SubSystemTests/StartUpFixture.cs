using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microservice.Api;
using Microservice.DataAccessEFCore;
using Microservice.DataAccessEFCore.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.SubSystemTests
{
    public sealed class StartUpFixture : IDisposable
    {
        private readonly IWebHost _webHost;
        private readonly string _containerId;
        private readonly string _imageName;

        public StartUpFixture()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.int.json", false, false)
                .Build();

            ConfigureStoryLineRest(config);

            (_containerId, _imageName) = ConfigureStoryLineWiremock(config);

            _webHost = ConfigureWebHost(config);

            AddInitialStateForDatabase();
        }

        public void ClearNicknamesInDatabase()
        {
            var context = _webHost.Services.CreateScope().ServiceProvider.GetService<NicknamesContext>();

            foreach (var entity in context.Nicknames)
            {
                context.Nicknames.Remove(entity);
            }

            context.SaveChanges();
        }

        private IWebHost ConfigureWebHost(IConfigurationRoot config)
        {
            var port = config.GetSection("Service")["Port"];
            var root = config.GetSection("Service")["Root"];

            return WebHost.CreateDefaultBuilder(null)
                .UseStartup<IntegrationStartup>()
                .UseSolutionRelativeContentRoot(root)
                .UseSetting("http_port", port)
                .ConfigureTestServices(services =>
                {
                    services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);
                })
                .ConfigureAppConfiguration((settings) =>
                    settings.AddJsonFile("appsettings.int.json", false, false))
                .Start();
        }

        private void ConfigureStoryLineRest(IConfigurationRoot config)
        {
            var host = config.GetSection("Service")["Host"];
            var port = config.GetSection("Service")["Port"];

            GodelTech.StoryLine.Rest.Config.AddServiceEndpont(
                "GodelTech.Microservices.IntegrationTests.Microservice.Api",
                $"{host}:{port}");

            GodelTech.StoryLine.Rest.Config.SetAssemblies(typeof(StartUpFixture).GetTypeInfo().Assembly);
        }

        private (string, string) ConfigureStoryLineWiremock(IConfigurationRoot config)
        {
            var host = config.GetSection("Wiremock")["Host"];
            var port = config.GetSection("Wiremock")["Port"];
            var image = config.GetSection("Wiremock")["Image"];
            var tag = config.GetSection("Wiremock")["Tag"];

            GodelTech.StoryLine.Wiremock.Config.SetBaseAddress($"{host}:{port}");

            return RunWiremockImage(port, image, tag);
        }

        private (string, string) RunWiremockImage(string port, string image, string tag)
        {
            var client = new DockerClientConfiguration()
                .CreateClient();

            var isImageCreated = CreateImage(client, image, tag);

            var containerId = client.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = image,
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    {
                        "8080", new EmptyStruct()
                    }
                },
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>> {
                        {
                            "8080", new List<PortBinding> {
                                new PortBinding { HostPort = port }
                            }
                        }
                    },
                }
            }).GetAwaiter().GetResult().ID;

            client.Containers.StartContainerAsync(containerId, new ContainerStartParameters()).GetAwaiter().GetResult();

            return (containerId, isImageCreated ? image : null);
        }

        private bool CreateImage(DockerClient client, string image, string tag)
        {
            if (!ImageExists(client, image))
            {
                client.Images.CreateImageAsync(
                    new ImagesCreateParameters
                    {
                        FromImage = image,
                        Tag = tag,
                    },
                    null, new Progress<JSONMessage>()).GetAwaiter().GetResult();

                return true;
            }

            return false;
        }

        private bool ImageExists(DockerClient client, string image)
        {
            return client.Images
                .ListImagesAsync(new ImagesListParameters())
                .GetAwaiter()
                .GetResult()
                .SelectMany(x =>x.RepoTags)
                .Any(x => x.StartsWith(image));
        }

        private void AddInitialStateForDatabase()
        {
            var context = _webHost.Services.CreateScope().ServiceProvider.GetService<NicknamesContext>();

            context.Users.Add(new User
            {
                Id = 1,
                Name = "User"
            });

            context.SaveChanges();
        }

        public void Dispose()
        {
            var client = new DockerClientConfiguration()
                .CreateClient();

            _webHost?.Dispose();

            if (!client.Containers.StopContainerAsync(_containerId, new ContainerStopParameters()).GetAwaiter()
                .GetResult())
            {
                client.Containers.KillContainerAsync(_containerId, new ContainerKillParameters()).GetAwaiter()
                    .GetResult();
            }

            client.Containers.RemoveContainerAsync(_containerId, new ContainerRemoveParameters()).GetAwaiter().GetResult();

            if (_imageName != null)
            {
                client.Images.DeleteImageAsync(_imageName, new ImageDeleteParameters()).GetAwaiter().GetResult();
            }
        }
    }
}