# Hospial
## Aplicação para gerenciamento de Hospital

Hospital é uma aplicação desenvolvida em c# com framework .NET 7, desenvolvida com Clean Architecture visando facilidade em seu desenvolvimento e aplicação de futuras features.
## Técnologias utilizadas.
O sistema foi desenvolvido com c#, framework .NET 7.
Seu banco de dados é SQLServer, Como orm foi utilizado o EF Core.
Testes escritos com xUnit.

#Bibliotecas utilizadas
  - libphonenumber-csharp. https://www.nuget.org/packages/libphonenumber-csharp
  - Moq. https://www.nuget.org/packages/Moq
  - Microsoft.EntityFrameworkCore.SqlServer. https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/8.0.0-rc.1.23419.6
  - Microsoft.EntityFrameworkCore.Design. https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/8.0.0-rc.1.23419.6
  - Microsoft.EntityFrameworkCore.Tools. https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/8.0.0-rc.1.23419.6
  - JQuery Mask Plugin. https://igorescobar.github.io/jQuery-Mask-Plugin/
## Features

- Hospital Management
    - Pasta onde toda a aplicação pode ser encontrada
    - Adapters, camada direcionada para banco de dados
    - Consumers, camada direcionada para Api´s, Aplicações Web, Desktop, no geral aquilo que irá consumir nossa aplicação. 
    - Core, o núcleo da apicação, contém Domain e Application onde todo o modelo do negócio é criado e sua devida aplicação feita
    - Tests, camada destinada a testes da aplicação

## Como configurar o projeto

O primeiro passo após clonar o projeto é executar suas migrations, abrindo o Console de Gerenciador de Pacotes
Exibir > Outras Janelas > Console do Gerenciador de Pacotes
E executar o comando Update-Database -project Data
Após a execução seu banco de dados estara pronto.

## Definindo HospitalWeb como Projeto de Inicialização
Caso o projeto de inicialização não seja HospitalWeb, será necessário definir como, para isso:
Abrar a solução
Expanda a pasta Consumers
Clique com o botão direito
E em seguida selecione a opção Definir como Projeto de Inicialização
Com isso basta apenas executar a aplicação.

## Primeiro acesso
Sendo a sua primeira vez acessando, será preciso criar um cadastro, com ele podendo ser Doutor ou Paciente.
Após preencher os dados no formulário, você sera redirecionado para a tela de login, basta selecionar o tipo de usuario e logo em seguida fornecer o acesso.
Pacientes devem informar o numero de celular enquanto Médicos o seu CRM.

## Paciente
Pacientes podem apenas visualizar suas próprias fichas.

## Médicos
Médicos podem criar, editar, visualizar e apagar fichas de qualquer Paciente do sistema.
