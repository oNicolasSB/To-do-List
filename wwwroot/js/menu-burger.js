let btnBurguer = window.document.querySelector('#btnBurger');
let menuBurguer = window.document.querySelector('#menuBurger');

btnBurguer.addEventListener('click', () => {
    btnBurguer.classList.toggle('active');
    menuBurguer.classList.toggle('active');
});