Os elementos do grupo são:
Francisco Miguel Amaro Maciel - 201100692
Hugo Miguel Ribeiro de Sousa – 201100690
Ricardo Daniel Soares da Silva - 201108043

-------------------------------------------

A solução é composta por 4 projetos:
-Client_UI (cliente, com interface gráfica)
-Manager (definição do objeto remoto)
-Server (servidor)
-Shared (definições partilhadas pelo cliente e servidor)

O sistema usa uma base de dados sqlite (ficheiro "diginotes.db"), que deve ser colocado no local de onde é executado o .exe do servidor.
Ou seja, correndo o servidor em modo debug a partir do Visual Studio, deverá estar em algo como ...\Server\bin\Debug.
É também neste diretório que é criado o ficheiro "log.txt", que contém os principais eventos que ocorrem no sistema.

O projeto Manager e Shared poderão precisar de ser compilados previamente, para atualizar a sua inclusão nos outros projetos.
Deve correr-se primeiro o Server, seguido dos Client_UI que se pretender.

Para testar, o sistema tem já algumas diginotes criadas nalgumas contas previamente criadas, que são as seguintes:
- teste1/teste1
- teste2/teste2
- teste3/teste3
- teste4/teste4