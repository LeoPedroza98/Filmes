# Filmes

**Teste Técnico - Grupo Soitic**
O teste consiste em duas partes, o back-end feito em ASP.NET Core e o front-end feito em Vue.Js.

 **API**

* Dependências: 
```
HtmlAgilityPack,
IdentityModel,
Microsoft.AspNetCore.Authentication.JwtBearer,
Microsoft.AspNetCore.JsonPatch,
Microsoft.AspNetCore.Mvc.NewtonsoftJson,
Microsoft.AspNetCore.OData,
Microsoft.EntityFrameworkCore,
Microsoft.EntityFrameworkCore.Abstractions,
Microsoft.EntityFrameworkCore.Design,
Microsoft.EntityFrameworkCore.Tools,
Microsoft.Extensions.Identity.Core,
Microsoft.Extensions.Logging.Debug,
Microsoft.VisualStudio.Web.CodeGeneration.Design,
Newtonsoft.Json,
Npgsql.EntityFrameworkCore.PostgreSQL
```

* FUNCIONAMENTO E CONFIGURAÇÃO API:
```
  * Você deverá possuir o PostgreSQL instalado. 
  * Para mudar a senha ,usuário, host e porta de acesso ao banco basta ir para o arquivo "appsettings.json" dentro da pasta "API" e mudar os valores de "Principal" para os que você utiliza normalmente.
  * Feito a mudança na conexção com o banco, basta selecionar no topo do VisualStudio no botão da execução a opção "Filmes.API" feito a mudança apenas execute o projeto que automaticamente será criado a "Database" no PostgreSQL.
```
* **Como criar e autenticar usuario via Postman ou Insomnia:**
```
  * Criação USUÁRIO
     Para inicio, é necessário cadastrar um usuário por meio de um "POST" para o Endpoint de Usuário (Apenas esse endpoint permite comandos enquanto a sessão não estiver autenticada):
     
      * "http://localhost:5378/api/usuario"
        {
          "login": "teste@gmail.com.br",
          "senha": "123@mudar",
          "nome": "Teste",
          "perfil":1
        }
```
```
  * Autenticação USUÁRIO
    Feito o cadastro, basta fazer Login dando um "POST" para o Endpoint de Autenticação:

    * "http://localhost:5378/api/autenticador/usuario"
      {
        "login": "teste@gmail.com.br",
        "senha": "123@mudar"
      }
```


 **FRONT-END**
 * Dependências: 
```
@sweetalert2/theme-borderless,
axios,
core-js,
jiff,
sweetalert2,
vue-sweetalert2,
vue-the-mask,
vuetify
```
* FUNCIONAMENTO E CONFIGURAÇÃO FRONT-END:
```
* Configuração
Necessário antes de tudo para executar em seu prompt de comando: 
"npm install"
Para que todas as dependências sejam instaladas

* Funcionamento

Após a execução do comando para instalar todas as dependências:
"npm run serve"
Para que o projeto rode após isso vai abrir a tela de login da aplicação para ter acesso é necessário colocar login e senha para obter o Front-End da Aplicação
```
