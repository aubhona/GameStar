# GameStar
Eng

Setting the Objective: Game Cycle
The main game:
1. The playing field shall be a green colored console screen;
2. The available game space is limited by a rectangular frame of black "_" and "|" symbols and is 2/4 of the console window horizontally and vertically, located in the center.
The game ends immediately, preventing player movement (and subsequent enemy movement, see below) if the user presses the Escape key during their turn.

Task Setting: Player Behavior
1. The player is represented by a black "*" symbol;
2. The player's starting position is the center of the playing field;
3. The player can move forward-backward, left-right using the WASD keys;
4. The player must not leave behind any trace of being in the past position (think about how you can make the previous position invisible on the console background);
5. A player may not "mash" the boundaries of the playing field. If a player attempts to go outside the left boundary of the field of play, the player is one unit to the left of the right boundary of the field of play. When trying to go beyond the lower boundary, the player is one unit below the upper boundary of the field. For transitions in opposite directions the behavior is similar.

Task Setting: Bizarre Enemy Behavior
1. The bizarre enemy is a white "#" symbol;
2. The starting position of a bizarre enemy is a random point within the field that does not match the starting position of other game entities;
3. A bizarre enemy takes a turn each time immediately after the player's turn;
4. The quirky enemy is moved randomly one unit up, down, left or right. Each of these moves is equally probable;
5. Like the player, the bizarre enemy can't go beyond the playing field and ends up at the opposite end when trying to cross them;
6. Similarly to a player, a bizarre enemy must leave no trace of being in a past position.

Setting the Objective: Finalizing the Game
1. As described earlier, the player can end the game on his turn by pressing the Escape key;
2. If, as a result of a player's move or an enemy's move, both characters end up at the same point, their designation is erased from the screen, a red "GAME OVER!" message is displayed in the center frame of the playing field, and the game ends:

Game Refinement: Enemy-Stalker
1. The Stalker is a red "@" symbol, stalking the player, constantly trying to get closer to him in either X or Y;
2. The Stalker moves one square on the X or Y axis per turn in the direction of the player immediately after a bizarre enemy. The axis that is closer to the player is selected for approach;
3. At the beginning of the game, the stalker appears in a random empty square on the map that does not match anything else;
4. Stalker can never be on the same square with other enemies. In case of being next to each other, the enemies cannot choose the points occupied by each other according to the results of the last moves;
5. Similar to facing a bizarre enemy, being on the same square with a stalker results in a "GAME OVER!";
6. Like the bizarre enemy, the stalker cannot go beyond the map boundary and operates under the same rules.

Refinement of the Game: Coin
1. A coin is a fixed object on the playing field, which can be picked up by both the player and enemies. It is indicated by a yellow "$" symbol;
2. At the beginning of the game, a coin appears in a random square of the field with nothing on it;
3. In the upper left corner of the screen is the text: "Player: <coin count>" and on the next line immediately below it, "Enemy: <coin count>";
4. When a player/enemy hits one cell with a coin, the corresponding counter is incremented by 1. The coin is removed and a new coin immediately appears on a random empty square.
5. If the player's/enemies' coin count becomes 5, the game ends immediately and the screen displays the message "Player wins!" or "Enemies win!" in red in the center, depending on the winner. *In this wording, it is assumed that the coin counter is shared by all enemies.
   
Refining the Game: Enemy Charger
1. Charger - A variation of a bizarre enemy that moves in a similar manner, equally likely to choose a random direction. It is denoted by a purple "%" symbol
2. Unlike the bizarre enemy, it moves not one square, but 5 squares in the chosen direction, leaving behind a gray poisoned trail of "%" symbols until the end of the next turn;
3. If a player steps on a gray trail, the game ends and the message "GAME OVER!" is displayed. Other enemies cannot step on the Charger's trace (they consider it an occupied cell).
4. A Charger cannot make a move in a given direction if there is already another enemy in its path;
5. If the charger, as a result of his move, passes through the player or ends up on the same square with him, the game ends and the message "GAME OVER!" is displayed.

___________________________________________________________________________________________________________________________________________________________________________________________________________________________________

Rus

Постановка Задачи: Игровой Цикл

Основная игра:
1.	Игровое поле должно представлять собой консольный экран зелёного цвета;
2.	Доступное игровое пространство ограничено прямоугольной рамкой из символов «_» и «|» чёрного цвета и составляет 2/4 консольного окна по горизонтали и по вертикали, расположено по центру.
Игра сразу завершается, предотвращая перемещение игрока (и последующее перемещение врага, см. далее), если пользователь нажал клавишу Escape во время своего хода.

Постановка Задачи: Поведение Игрока

1.	Игрок представляет собой чёрный символ «*»;
2.	Начальная позиция игрока – центр игрового поля;
3.	Игрок может перемещаться вперёд-назад, влево-вправо при помощи клавиш WASD;
4.	Игрок не должен оставлять за собой никаких следов нахождения на прошлой позиции (подумайте, как можно сделать предыдущую позицию невидимой на консольном фоне);
5.	Игрок не может «затирать» собой границы игрового поля. При попытке выбраться за границу игрового поля слева игрок оказывается на единицу левее правой границы поля. При попытке выйти за нижнюю границу, игрок оказывается на единицу ниже верхней границей поля. Для переходов в противоположные стороны поведение аналогичное.

Постановка Задачи: Поведение Причудливого Врага
1.	Причудливый враг представляет собой белый символ «#»;
2.	Начальная позиция причудливого врага – случайная точка внутри поля, не совпадающая со стартовой позицией других игровых сущностей;
3.	Причудливый враг осуществляет ход каждый раз сразу после хода игрока;
4.	Перемещение причудливого врага осуществляется случайным образом на единицу вверх, вниз, влево или вправо. Каждое из этих перемещений равновероятно;
5.	Как и игрок, причудливый враг не может выходить за рамки игрового поля и при попытке пересечь их оказывается в противоположном конце; 
6.	Аналогично игроку причудливый враг не должен оставлять за собой никаких следов нахождения на прошлой позиции.

Постановка Задачи: Завершение Игры
1.	Как описывалось ранее, игрок может закончить игру на своём ходу по нажатию клавиши Escape;
2.	Если в результате хода игрока или хода врага оба персонажа оказываются в одной точке, их обозначение стирается с экрана, в рамке игрового поля по центру выводится красное сообщение “GAME OVER!”, а игра завершается:

Доработка Игры: Враг-Сталкер
1.	Сталкер представляет собой красный символ «@», преследует игрока, постоянно стараясь приблизиться к нему либо по X, либо по Y;
2.	Сталкер двигается на одну клетку по оси X или по оси Y за ход в направление игрока сразу после причудливого врага. Для приближения выбирается та ось, по которой до игрока добраться ближе;
3.	В начале игры сталкер появляется в случайной пустой клетке карты, не совпадающей ни с чем другим;
4.	Сталкер никогда не может оказаться на одной клетке с другими врагами. В случае нахождения рядом враги не могут выбрать занятые друг другом точки по результатам последних ходов;
5.	Аналогично столкновению с причудливым врагом, нахождение на одной клетке со сталкером приводит к “GAME OVER!”;
6.	Как и причудливый враг, сталкер не может выйти за границу карты и действует по тем же правилам.

Доработка Игры: Монетка
1.	Монетка – неподвижный объект на игровом поле, который может подбирать как игрок, так и враги. Обозначается жёлтым символом «$»;
2.	В начале игры монетка появляется в случайной клетке поля, на которой ничего не стоит;
3.	В левом верхнем углу экрана отображается текст: “Player: <coin count>”, а на следующей строке сразу под ним – “Enemy: <coin count>”;
4.	При попадании игрока/врага на одну клетку с монеткой соответствующий счётчик увеличивается на 1. При этом монетка удаляется, а новая сразу же появляется на случайной пустой клетке.
5.	Если счётчик монеток игрока/врагов становится равен 5, игра сразу же завершается, а на экране красным цветом по центру выводится сообщение “Player wins!” или “Enemies win!” в зависимости от победитель.
*В данной формулировке считается, что счётчик монет у всех врагов общий.


Доработка Игры: Враг-Чарджер
1.	Чарджер – вариация причудливого врага, которая перемещается аналогичным образом, равновероятно выбирая случайное направление. Обозначается фиолетовым символом «%»
2.	В отличие от причудливого врага, движется уже не на одну клетку, а на 5 в выбранном направлении, оставляя за собой серый отравленный след из символов «%» до конца следующего хода;
3.	При наступлении игрока на серый след игра завершается и выводится сообщение “GAME OVER!”. Другие враги не могут наступать на след чарджера (считают его занятой клеткой).
4.	Чарджер не может совершить движение в заданном направлении, если на его пути уже есть другой враг;
5.	Если чарджер в результате своего хода проходит через игрока насквозь или оказывается с ним на одной клетке, игра завершается и выводится сообщение “GAME OVER!”.

