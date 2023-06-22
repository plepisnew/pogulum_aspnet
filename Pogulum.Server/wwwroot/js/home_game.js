const gameCarousel = document.querySelector(".carousel.game");
const gameCarouselItems = document.querySelector(".carousel-items.game");
const gameAllItems = document.querySelectorAll(".carousel-item.game");

const gameColumns = 3;
const gameItemCount = gameCarouselItems.children.length;

let gameCarouselWidth = 0;
let gameItemWidth = 0;

let gameCurrentItem = 0;

const gameDecrement = () => {
  if (gameCurrentItem == 0) gameCurrentItem = gameItemCount - gameColumns;
  else gameCurrentItem--;
  gameSyncMargin();
};

const gameIncrement = () => {
  if (gameCurrentItem == gameItemCount - gameColumns) gameCurrentItem = 0;
  else gameCurrentItem++;
  gameSyncMargin();
};

document.querySelector(".carousel-left-btn.game").onclick = gameDecrement;

document.querySelector(".carousel-right-btn.game").onclick = gameIncrement;

const gameSyncMargin = () => {
  gameCarouselItems.style[
    "margin-left"
  ] = `calc(-${gameCarouselWidth} / ${gameColumns} * ${gameCurrentItem})`;
};

const gameOnresize = () => {
  gameCarouselWidth = `${gameCarousel.getBoundingClientRect().width}px`;
  gameItemWidth = `calc(${gameCarouselWidth}/${gameColumns})`;
  gameSyncMargin();
  gameAllItems.forEach((item) => (item.style.width = gameItemWidth));
};

window.onresize += gameOnresize;

setInterval(gameIncrement, 9000);

gameOnresize();
