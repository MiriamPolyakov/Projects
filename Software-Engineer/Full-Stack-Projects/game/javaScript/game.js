var charactor = document.getElementById("charactor");
var game = document.getElementById("game");
var interval;
var both = 0;
var counter = 0;
var currentBlocks = [];
let travel = 0.3;
let travel_ball = 2;



function moveLeft() {
    var left =
        parseInt(window.getComputedStyle(charactor).getPropertyValue("left"));
        if (left > 0) {
        charactor.style.left = left - travel_ball + "px";
    }
}
function moveRight() {
    var left =
        parseInt(window.getComputedStyle(charactor).getPropertyValue("left"));
    if (left < 560) {

        charactor.style.left = left + travel_ball + "px";
    }
}

document.addEventListener("keydown", event => {
    if (both == 0) {
        both++;
        if (event.key === "ArrowLeft") {
            interval = setInterval(moveLeft, 2);
        }
        if (event.key === "ArrowRight") {
            interval = setInterval(moveRight, 2);
        }
    }
});
document.addEventListener("keyup", event => {
    clearInterval(interval);
    both = 0;
});

document.getElementById("lose").innerHTML = "you are in level 1!!!!"

var blocks = setInterval(function () {
    document.getElementById("score").innerHTML = "Score: " + (counter - 7);
    
    //תפיסת הבלוק האחד לפני הבלוק שנוצר
    var blockLast = document.getElementById("block" + (counter - 1));
    var holeLast = document.getElementById("hole" + (counter - 1));
    if (counter > 0) {
        var blockLastTop =
            parseInt(window.getComputedStyle(blockLast).getPropertyValue("top"));
        var holeLastTop =
            parseInt(window.getComputedStyle(holeLast).getPropertyValue("top"));
    }

    if (blockLastTop < 710 || counter == 0) {
        var block = document.createElement("div");
        var hole = document.createElement("div");
        block.setAttribute("class", "block");
        hole.setAttribute("class", "hole");
        block.setAttribute("id", "block" + counter);
        hole.setAttribute("id", "hole" + counter);
        block.style.top = blockLastTop + 100 + "px";
        hole.style.top = holeLastTop + 100 + "px";
        var random = Math.floor(Math.random() * 360);
        hole.style.left = random + "px";
        game.appendChild(block);
        game.appendChild(hole);
        currentBlocks.push(counter);
        counter++;
    }
    var charactorTop =
        parseInt(window.getComputedStyle(charactor).getPropertyValue("top"));
    var charactorLeft =
        parseInt(window.getComputedStyle(charactor).getPropertyValue("left"));

    if (charactorTop <= 0) {
        clearInterval(blocks);
        location.reload;
        document.getElementById("lose").innerHTML = " ";
        document.getElementById("win").innerHTML = "GAME OVER..."
    }

    var drop = 0;

    for (var i = 0; i < currentBlocks.length; i++) {
        let current = currentBlocks[i];

        let iblock = document.getElementById("block" + current);
        let ihole = document.getElementById("hole" + current);

        let iblockTop =
            parseFloat(window.getComputedStyle(iblock).getPropertyValue("top"));
        let iholeLeft =
            parseFloat(window.getComputedStyle(ihole).getPropertyValue("left"));

        iblock.style.top = iblockTop - travel + "px";
        ihole.style.top = iblockTop - travel + "px";

        if (iblockTop < -20) {
            currentBlocks.shift();
            iblock.remove();
            ihole.remove();
        }

        if (iblockTop - 20 < charactorTop && iblockTop > charactorTop) {
            drop++;

            if (iholeLeft <= charactorLeft && iholeLeft + 30 >= charactorLeft) {
                drop = 0;
            }
        }
    }

    if (drop == 0) {
        if (charactorTop < 560) {
            charactor.style.top = charactorTop + travel_ball + "px";
        }
    } else {
        charactor.style.top = charactorTop - travel + "px";
    }


    if (counter === 28) {
        document.getElementById("lose").innerHTML = "you are in level 2!!!!";
        travel = 0.5;
        travel_ball = 4;
    }
    if (counter === 48) {
        document.getElementById("lose").innerHTML = "you are in level 3!!!!";
        travel = 0.75;
        travel_ball = 6;
    }
    if (counter === 68) {
        document.getElementById("lose").innerHTML = "you are in level 4!!!!";
        travel = 0.95;
        travel_ball = 6;
    }
    if (counter === 88) {
        document.getElementById("lose").innerHTML = "you are in level 5!!!!";
        travel = 1.2;
        travel_ball = 8;
    }
    if (counter === 108) {
        document.getElementById("lose").innerHTML = "";
        document.getElementById("win").innerHTML = "YOU ARE THE WINNER!!!!";
        clearInterval(blocks);
        location.reload;
    }
}, 1);