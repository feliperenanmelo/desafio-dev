# Desafio programação - para vaga desenvolvedor

O projeto Bycoders.DesafioDev consiste em em frontend MVC e uma API de processamento de transações financeiras através de um arquivo TXT,
desenvolvido baseados em boas práticas de programação, arquitetura flexível para implementação de novas features, clean code e uma série de outras complexidades.

# Bycoders.DesafioDev.API
# Api de transações desenvolvida utilziando NET 6.0 e entity frameqorkcore 6.0, utilizando os recursos possíveis de paginação e mapeamento de dados para facilitação de trabalho com banco de dados SQL SERVER

| Método  | Enpoint                          | Body | Parameters       | Descrição                                       |
| ------- | ------------------               | -----| ---------------- | ----------------------------------------------- |
| GET     | /api/desafio-dev                 |      |  pageSize , page | Parametros passados na rota para paginação      |
| POST    | /api/desafio-dev                 | file |                  | Parametro tipo form/data para envio de arquivos |
| GET     | /api/desafio-dev/tipos-transacao |      | pageSize , page  | Parametros passados na rota para paginação      |

# Bycoders.DesafioDev.App
# App MVC desenvolvido em NET 6.0 com uma tela para importação do arquivo e outra tela para exibição de todos os detalhes das importações

Execução do projeto

Acesse o caminho :
# desafio-dev/Bycoders.DesafioDev/docker/ e execute o comando docker-compose -f bycorders-production.yaml up

Serão criados os serviços de SQL Server, Api que ao subir cria o banco de dados e insere os dados padrõs de tipos de transação, app mvc.
