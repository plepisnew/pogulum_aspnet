const broadcasterCarousel = document.querySelector(".carousel.broadcaster");
const broadcasterCarouselItems = document.querySelector(
  ".carousel-items.broadcaster"
);
const broadcasterAllItems = document.querySelectorAll(
  ".carousel-item.broadcaster"
);

const broadcasterColumns = 3;
const broadcasterItemCount = broadcasterCarouselItems.children.length;

let broadcasterCarouselWidth = 0;
let broadcasterItemWidth = 0;

let broadcasterCurrentItem = 0;

const broadcasterDecrement = () => {
  if (broadcasterCurrentItem == 0)
    broadcasterCurrentItem = broadcasterItemCount - broadcasterColumns;
  else broadcasterCurrentItem--;
  broadcasterSyncMargin();
};

const broadcasterIncrement = () => {
  if (broadcasterCurrentItem == broadcasterItemCount - broadcasterColumns)
    broadcasterCurrentItem = 0;
  else broadcasterCurrentItem++;
  broadcasterSyncMargin();
};

document.querySelector(".carousel-left-btn.broadcaster").onclick =
  broadcasterDecrement;

document.querySelector(".carousel-right-btn.broadcaster").onclick =
  broadcasterIncrement;

const broadcasterSyncMargin = () => {
  broadcasterCarouselItems.style[
    "margin-left"
  ] = `calc(-${broadcasterCarouselWidth} / ${broadcasterColumns} * ${broadcasterCurrentItem})`;
};

const broadcasterOnresize = () => {
  broadcasterCarouselWidth = `${
    broadcasterCarousel.getBoundingClientRect().width
  }px`;
  broadcasterItemWidth = `calc(${broadcasterCarouselWidth}/${broadcasterColumns})`;
  broadcasterSyncMargin();
  broadcasterAllItems.forEach(
    (item) => (item.style.width = broadcasterItemWidth)
  );
};

window.onresize += broadcasterOnresize;

setInterval(broadcasterIncrement, 14000);

broadcasterOnresize();
