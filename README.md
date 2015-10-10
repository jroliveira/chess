# chess

[![Build status](https://ci.appveyor.com/api/projects/status/h516hk65yj3fmypr/branch/master?svg=true)](https://ci.appveyor.com/project/junioro/chess/branch/master)
[![Build Status](https://travis-ci.org/jroliveira/chess.svg?branch=master)](https://travis-ci.org/jroliveira/chess)
[![Coverage Status](https://coveralls.io/repos/jroliveira/chess/badge.svg?branch=master&service=github)](https://coveralls.io/github/jroliveira/chess?branch=master)

## O que é?

É um jogo de Xadrez feito em C#.  
A aplicação roda em Mono e .NET Framework.  

![Image of Game](https://github.com/jroliveira/chess/blob/master/docs/game.png)

## Partes da aplicação

### Chess

É o core do jogo.

A interface `IGame` contém as definições para jogar e a classe `Game` implementa este contrato.
  
#### Chess.Multiplayer

É uma extensão da biblioteca **Chess**, que permite jogar com outro jogador.

A interface `IGameMultiplayer` extende a interface `IGame` e nela contém as definições para jogar com outro jogador.
A classe `GameMultiplayer` implementa o contrato `IGameMultiplayer` e herda da classe `Game`.

#### Chess.UI.Console

É a interface do jogo que utiliza a classe `GameMultiplayer` para acessar as funcionalidades do jogo.

## Para instalar

### Instalar a fonte DejaVu Sans Mono

Esta fonte permite mostrar as peças do Xadrez no console do Windows, para instalar vamos seguir os passos abaixo.

 - Baixe o arquivo **dejavu-fonts-ttf-2.35.zip** do site [dejavu-fonts.org](http://dejavu-fonts.org/wiki/Download).
 - Extraia o arquivo .zip baixado e instale a fonte **DejaVuSansMono.ttf** que está na pasta **ttf**.
 - Abra o **regedit** vá até `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont`.
 - Adicione um novo **Valor da Cadeia de Caracteres** `000` com o valor `DejaVu Sans Mono`.

### Rodar a aplicação

 - `git clone https://github.com/jroliveira/chess.git`
 - Rode o projeto, na janela do console vá em Propriedades -> Fonte e escolha a fonte **DejaVu Sans Mono** e tamanho **36**.

## Para contribuir 

1. Fork it
2. git checkout -b <branch-name>
3. git add --all && git commit -m "feature description"
4. git push origin <branch-name>
5. Create a pull request
