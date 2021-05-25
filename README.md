# RewindTest
## Projeto pessoal realizado para se testar uma mecânica de rewind, onde certos movimentos do player são gravados e tocados novamente ou de modo reverso, se dando a impressão de voltar no tempo dentro do jogo

-Sobre:
=====================================

- Geral:

O projeto se iniciou com o objetivo de se criar uma mecânica de rewind baseada na personagem Tracer de Overwatch, porém com o personagem visível e refazendo os passos e ações que foram realizados. Além do rewind outros pontos foram estudados nesse projeto como o uso de scriptable objects para o armazenamento de tipo de cada objeto, e com isso a ação que será realizada com cada, como carregá-lo, quebrá-lo ou arremessá-lo será realizada.

- Pontos de aprendizado:

Para a elaboração da mecânica de rewind se utilizou do método Command Pattern que consiste em se criar um script separado para alguma ação que se irá realizar (no caso desse projeto as ações são as de movimentação e de ação com objetos), e para cada ação se cria uma instância do script, e se chama o método que faz realizar a ação, dessa maneira como cada parte do movimento é determinada por uma instância em particular essas podem ser armazenadas e chamadas novamente para se realizar a repetição das ações, as ações são salvas em uma lista única que armazena tanto movimentos quanto ações em objetos por meio de polimorfismo que uma interface em comum proporciona para eles. A movimentação é determinada por meio dos inputs que ela recebe e a posição no momento da ação, e a ação em objetos é determinada pelo player, o objeto, e o status de interação entre player e objeto, que observa se o id em interação com o player e o mesmo do objeto que está verificando a ação.

Cada objeto possui sua ação própria relacionada ao seu tipo, que é pré-determinado em seu scriptable object.

