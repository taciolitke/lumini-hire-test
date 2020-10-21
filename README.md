# Lumini - Elasticsearch

### Technologies
* Dotnet core 3.1
* Docker.
* SQLite
* Bootstrap
* MVC
* ASP.NET Identity
* Elasticsearch
* Kibana

## Rodando o projeto
Basta navegar até a raiz do projeto e rodar docker-compose up

Após rodar subir o projeto com docker-compose é possivel acessar as seguintes aplicações:

# Pesquisa - pública

http://localhost:5000/

# Dashboard Kibana

http://localhost:5601/app/kibana#/management/elasticsearch/index_management/indices

# ElasticSearch

http://localhost:9200/


# Carga inicial do ElasticSearch - Manual

O comando abaixo irá popular o ElasticSerach com o arquivo CollegeScorecard_Raw_Data.zip

Esse mesmo arquivo pode ser encontrado na pasta src\LuminiHire.ElasticSearch.Seed\sample_data

Comando manual:
dotnet run --project src/LuminiHire.ElasticSearch.Seed

# Observações:
Não foram mapeados todos os campos do csv, já que com N campos ou N + X campos já da para seguir o fluxo inteiro do teste.
