# GodelTech.GraphQl

### How to run?
- Open solution in VS or Rider
- Run the Web project
- Open `https://localhost:5001/graphql`

### How to run queries?
- In Altair - open `docs` section. There will be 1 query and 1 mutation

Query:
```
query{
  properties{
    agentAddress
    agentName
    agentPhone
    category
    country
    countryCode
    county
    description
    detailsUrl
    firstPublishedDate
    id
    imageUrl
    lastPublishedDate
    latitude
    longitude
    note
    numBathrooms
    numBedrooms
    numFloors
    postTown
    price
    priceChanges {
      dateTime
    }
    propertyType
    shortDescription
    status
    streetName
  }
}
```

Mutation:
```
mutation{
  createOrUpdatePropertyNote(propertyNote: { propertyId: "id", note: "note" } ) 
}
```