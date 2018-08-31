# Sistema Básico de Restaurantes - Back-end

## 1. Passos para começar

### Clonando o Repositório

`git clone https://github.com/rfopaulino/basic-restaurant-back.git`

### Configurando o banco de dados

Acessar os arquivos Interface > appsettings.json e Infrastructure > appsettings.json e configurar a string de conexão

### Preparando a solução

Abrir um prompt de sua preferência e executar na ordem os comandos `dotnet restore` `dotnet build`

Em seguida, ainda pelo prompt, acesse a pasta Infrastructure e execute o comando `dotnet ef database update`

E finalmente para subir a aplicação e tê-la rodando, vá até a pasta Interface e execute o comando `dotnet run`

Pronto, se tudo correu bem, aparecerá no prompt a url na qual os serviços podem ser consumidos. Ou seja, a aplicação está rodando!