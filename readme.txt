Os elementos do grupo s�o:
Francisco Miguel Amaro Maciel - 201100692
Hugo Miguel Ribeiro de Sousa � 201100690
Ricardo Daniel Soares da Silva - 201108043

-------------------------------------------

A solu��o � composta por 4 projetos:
-Client_UI (cliente, com interface gr�fica)
-Manager (defini��o do objeto remoto)
-Server (servidor)
-Shared (defini��es partilhadas pelo cliente e servidor)

O sistema usa uma base de dados sqlite (ficheiro "diginotes.db"), que deve ser colocado no local de onde � executado o .exe do servidor.
Ou seja, correndo o servidor em modo debug a partir do Visual Studio, dever� estar em algo como ...\Server\bin\Debug.
� tamb�m neste diret�rio que � criado o ficheiro "log.txt", que cont�m os principais eventos que ocorrem no sistema.

O projeto Manager e Shared poder�o precisar de ser compilados previamente, para atualizar a sua inclus�o nos outros projetos.
Deve correr-se primeiro o Server, seguido dos Client_UI que se pretender.

Para testar, o sistema tem j� algumas diginotes criadas nalgumas contas previamente criadas, que s�o as seguintes:
- teste1/teste1
- teste2/teste2
- teste3/teste3
- teste4/teste4