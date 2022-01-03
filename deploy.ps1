
function Publish-Demo {
  Set-PSDebug -Trace 1
  az group create --name rg-cosmosdb-demo-canada-01 --location canadacentral --tags "course=az-204"
  az cosmosdb create --name cosmos-cosmosdb-demo-canada-01 --resource-group rg-cosmosdb-demo-canada-01
  
  # Get connection string from azure and update the app configuration
  $uriEndpoint = $(az cosmosdb show --name cosmos-cosmosdb-demo-canada-01 --resource-group rg-cosmosdb-demo-canada-01 --query documentEndpoint)
  $primaryKey = $(az cosmosdb keys list --name cosmos-cosmosdb-demo-canada-01 --resource-group rg-cosmosdb-demo-canada-01 --query primaryMasterKey)

  dotnet user-secrets set "EndpointUri" "$uriEndpoint"
  dotnet user-secrets set "PrimaryKey" "$primaryKey"
  Set-PSDebug -Trace 0
}

function Unpublish-Demo {
  Set-PSDebug -Trace 1
  az group delete --name rg-cosmosdb-demo-canada-01 --yes
  Set-PSDebug -Trace 0
}