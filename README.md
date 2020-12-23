# Minesweeper_WPF
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
   
</head>
<body>
    <h1>
        Desktop game Minesweeper (Сапер) [Wpf(MVVM)]
    </h1>
 <p style="text-indent: 20px">
        Implementation of the Minesweeper game on the WPF (MVVM) platform. 
        <br />
        Game rules:
    </p>
   <ul>
       <li> The playing field consists of cells (dimension is set in the parameters (game level)) </li>
       <li> 1) The number in the cell shows how many mines are hidden around this
                cells. This number will help you understand where you are
                 safe cells, and where the bombs are. </li>
       <li>
          If there is an empty cell next to an open cell, it is
                will open automatically.
       </li>
       <li> If you open a cell with a mine, the game is lost ..
                To mark the cell containing the bomb, click
                her right mouse button.</li>
		<li>
          		The game continues until you open everything
                unmined cells
      		 </li>
   </ul>
    
  
</body>
</html>
