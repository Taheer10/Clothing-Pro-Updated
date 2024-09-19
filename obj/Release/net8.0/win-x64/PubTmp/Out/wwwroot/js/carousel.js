let currentSlide = 0;

function nextSlide() {
    const wrapper = document.querySelector('.carousel-wrapper');
    const totalSlides = wrapper.children.length;
    const cardWidth = wrapper.children[0].offsetWidth + 15; // card width + margin-right
    const maxSlide = totalSlides - 3; // Adjust based on the number of visible cards

    if (currentSlide < maxSlide) {
        currentSlide++;
        wrapper.style.transform = `translateX(-${currentSlide * cardWidth}px)`;
    }
}

function prevSlide() {
    const wrapper = document.querySelector('.carousel-wrapper');
    const cardWidth = wrapper.children[0].offsetWidth + 15; // card width + margin-right

    if (currentSlide > 0) {
        currentSlide--;
        wrapper.style.transform = `translateX(-${currentSlide * cardWidth}px)`;
    }
}


let currentPantsSlide = 0;

function nextPantsSlide() {
    const wrapper = document.querySelector('.carousel-wrapper-pants');
    const totalSlides = wrapper.children.length;
    const cardWidth = wrapper.children[0].offsetWidth + 15; // card width + margin-right
    const maxSlide = totalSlides - 3; // Adjust based on the number of visible cards

    if (currentPantsSlide < maxSlide) {
        currentPantsSlide++;
        wrapper.style.transform = `translateX(-${currentPantsSlide * cardWidth}px)`;
    }
}

function prevPantsSlide() {
    const wrapper = document.querySelector('.carousel-wrapper-pants');
    const cardWidth = wrapper.children[0].offsetWidth + 15; // card width + margin-right

    if (currentPantsSlide > 0) {
        currentPantsSlide--;
        wrapper.style.transform = `translateX(-${currentPantsSlide * cardWidth}px)`;
    }
}
