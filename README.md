<H1 align="center">Estrutura Login e Autenticação OWIN</H1>
<p align="center">🚀 Projeto de criação de uma estrutura utilizando Autenticação OWIN para referências futuras</p>

## Recursos Utilizados

* OWIN

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

 ## Criação do Banco de Dados.

 Criar Banco de Dados DBUsuarios.


 ## Inicialização de aplicativo
 
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

 ## Controllers

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

## Resultado

### Login 

<img src="https://cdn.discordapp.com/attachments/1046824853015113789/1204173567449628823/image.png?ex=65d3c506&is=65c15006&hm=ffb439efc2b5e44dc2ac4b9333ceaeb27277008ccea1f1deb0e65f810363860c&" alt="">

### Cadastro 

<img src="https://cdn.discordapp.com/attachments/1046824853015113789/1204173773490880572/image.png?ex=65d3c538&is=65c15038&hm=bc1ae3325eb5de0795543a2319cb3197abcc7f2de6c36c4022acf3e9444827a5&" alt="">

### Registro realizado com sucesso 

<img src="https://cdn.discordapp.com/attachments/1046824853015113789/1204174080765329468/image.png?ex=65d3c581&is=65c15081&hm=a7458a1b311f831419d0ed8a63476bab4351f0955165995e21b1093cfd42e6b0&" alt="">

### Autenticado

<img src="https://cdn.discordapp.com/attachments/1046824853015113789/1204174208116985896/image.png?ex=65d3c59f&is=65c1509f&hm=f4f68f46a17c6500992afd2f1e85e80aaebf65b609c792987f525dc26525427d&" alt="">





