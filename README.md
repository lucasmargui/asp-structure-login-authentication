<H1 align="center">Estrutura Login e Autenticação OWIN</H1>
<p align="center">🚀 Projeto de criação de uma estrutura utilizando Autenticação OWIN para referências futuras</p>

## Recursos Utilizados

* OWIN

 ## Execução do Entity Framework nas IDE's: VS 2015/2017:
<details>
  <summary>Clique para mostrar conteúdo</summary>
   Ao realizar os comandos:
 
  ```
    Enable-Migrations
  ```
  e
  
  ```
    Update-Database -Verbose
  ```
  
Nas versões mais recentes do Visual Studio (2015/2017), se faz necessário criar uma nova instância do localdb do sql no seu computador. A qual poderá ser criado da seguinte maneira:

Passo 1: Abrir o cmd e executar o seguinte comando:
  ```
  SqlLocalDB.exe create "Local"
  ```
Passo 2: Executar a instance com seguinte comando:
  ```
  SqlLocalDb.exe start
  ```
  
Passo 3: Ir até o 'Package Manager Console' e executar o seguinte comando:
  ```
  Update-Database -Verbose
  ```

## Alteração da String de conexão

```
Web.Config
```
 Alterar string de conexão para fazer conexão entre Entity framework e o banco de dados
 
```
  <connectionStrings>
    <add name="Cadastro"
         connectionString="Data Source= (localdb)\Local;Initial Catalog=DbUsuarios;Integrated Security=True;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>

```


</details>



 


 ## Criação do Banco de Dados.

 Criar Banco de Dados DBUsuarios.


 ## Inicialização de aplicativo

 <details>
  <summary>Clique para mostrar conteúdo</summary>
  
Configuração da inicialização do aplicativo implementando OWIN

```
Startup.cs
```



O antifogery token é um sistema para identificar o usuário e evitar que sites de terceiros enviem esse formulário, ele precisa de um identificador único para identificar que é aquele usuário que esta enviando o “form”, então aqui estamos passando oque está no claim como identificador único que foi definido no Controller Autenticacao na action Login
```
 app.UseCookieAuthentication(new CookieAuthenticationOptions
 {
 AuthenticationType = "ApplicationCookie",
 LoginPath = new PathString("/Autenticacao/Login")
 });

 AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login";

```

</details>
 


 ## Controllers

 <details>
  <summary>Clique para mostrar conteúdo</summary>
  
### AutenticacaoController.cs

Responsável pelo Cadastro e Login, criando um cookie de autenticação através do Claim com Nome e Login
```
Controllers/AutenticacaoController.cs
```

Criando identity para geração do cookie de autenticação no Login

```
var identity = new ClaimsIdentity(new[]
{
 new Claim(ClaimTypes.Name, usuario.Nome),
 new Claim("Login", usuario.Login)
}, "ApplicationCookie");

Request.GetOwinContext().Authentication.SignIn(identity);

```

Destruindo cookie de autenticação através do Logout

```
Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
```

  ### PainelController.cs

Utilização do authorize, caso a pessoa não esteja autenticada pelo OWIN ela será redirecionada pelo caminho especificado na Startup.cs criado na pasta raiz

```
Controllers/PainelController.cs
```

  ### PerfilController.cs

  Controller utilizado para realizar ações quando o usuário estiver autorizado, como implementação de alteração de senha.

```
Controllers/PainelController.cs
```

Pega o usuário que está autenticado no Owin

```
var identity = User.Identity as ClaimsIdentity;
```

  
Identity possui varios claims mas no Autenticacao/login existe um claim com login e nome que foram passados para criação do cookie de autenticação

```
var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;
```

</details>


  

## Resultado

<div align="center">
 <h3>Login</h3>
<img src="https://github.com/lucasmargui/ASP_Login_Autenticacao/assets/157809964/eadef139-4faa-466b-bc60-89ba467d175d" style="width:100%">
</div>


<div align="center">
  <h3>Cadastro</h3>
<img src="https://github.com/lucasmargui/ASP_Login_Autenticacao/assets/157809964/f8c91505-2b2f-4a57-8ca4-06f0cb0cf38c" style="width:100%">
</div>


<div align="center">
 <h3>Registro realizado com sucesso</h3>
<img src="https://github.com/lucasmargui/ASP_Login_Autenticacao/assets/157809964/a70603a4-4f62-4f15-a3ed-8c8de43f84e8" style="width:100%">
</div>



<div align="center">
 <h3>Autenticado</h3>
<img src="https://github.com/lucasmargui/ASP_Login_Autenticacao/assets/157809964/f8546b03-49b1-4725-b153-e88e2cd0941d" style="width:100%">
</div>






