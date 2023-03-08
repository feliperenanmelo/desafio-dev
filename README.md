# Desafio programação - para vaga desenvolvedor

O projeto Bycoders.DesafioDev consiste em em frontend MVC e uma API de processamento de transações financeiras através de um arquivo TXT,
desenvolvido baseados em boas práticas de programação, arquitetura flexível para implementação de novas features, clean code, utilizando resiliência com polly para implemntação do pattern de retry e cirtuit breaker, padronização de respotas das APIs 

# Bycoders.DesafioDev.API
Api de transações desenvolvida utilziando NET 6.0 e entity frameqorkcore 6.0 para manipulação dos dados

| Método  | Enpoint                          | Body | Parameters       | Descrição                                       |
| ------- | ------------------               | -----| ---------------- | ----------------------------------------------- |
| POST    | /api/desafio-dev                 | file |                  | Parametro tipo form/data para envio de arquivos |

# Bycoders.DesafioDev.App
App MVC desenvolvido em NET 6.0 com usso de bootstrap com uma tela para importação do arquivo e outra tela para exibição de todos os detalhes das importações com sucesso e erros.

# Execução do projeto

Acesse o caminho :
desafio-dev/Bycoders.DesafioDev/docker/ e execute o comando 
# docker-compose -f bycorders-production.yaml up

Após executar com sucesso o comando e criar os containers bycorders-desafio-dev-sql-server, bycorders-desafio-dev-backend e bycorders-desafio-dev-front,  você poderá acessar através de http://localhost:6010 a aplicação.

# Execução dos testes

Os testes automatizados foram feitos com uso de selenium web driver, para execução é necessário acessar o site https://chromedriver.chromium.org/downloads e baixar o chrome drive compatível com seu browser chrome

Para saber sua versão de chrome basta acessar o chrome la lateral direita, clicar sobre os três pontinhos, depois ajuda em seguida Sobre o Google Chrome.
Para o download você pode não achar a versão exata de driver, basta esolher a que mais se aproximar do seu.
Ao fazer o download descompacte o arquivo até ver o chromedriver.exe.

Acesse o caminho :
desafio-dev\Bycoders.DesafioDev\tests\Bycoders.DesafioDev.Tests e procure pelo arquivo appsettings.json
Preencha o campo WebDriver o caminho para chegar no arquivo chromedriver.exe
Ex: E:\\webDriver\\

Volte ao caminho desafio-dev\Bycoders.DesafioDev\tests\Bycoders.DesafioDev.Tests e execute o comando 
# dotnet test

Tudo dando certo os testes automatizados e unitários serão executados
