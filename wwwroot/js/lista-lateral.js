let btnAbrir = window.document.querySelector('#abrirListaLateral');
let btnFechar = window.document.querySelector('#fecharListaLateral');
let listaLateral = window.document.querySelector('#listaLateral');

window.addEventListener("DOMContentLoaded", ajustarListaLateral)
window.addEventListener("resize", ajustarListaLateral)


function ajustarListaLateral() {
    if(window.innerWidth < 992) {
        if(listaLateral != null) {
            if(listaLateral.classList.contains('normal')) {
                listaLateral.classList.remove('normal');
            }
        }
    } else {
        if(listaLateral != null) {
            if(listaLateral.classList.contains('active')) {
                listaLateral.classList.remove('active');
            }
            if(!listaLateral.classList.contains('normal')) {
                listaLateral.classList.add('normal');
            }
    
        }
    }
}

if(btnAbrir!=null) {
    btnAbrir.addEventListener('click', () => {
        if(!listaLateral.classList.contains('active'))
        listaLateral.classList.add('active');
    })
}

if(btnFechar!=null) {
    btnFechar.addEventListener('click', () => {
        if(listaLateral.classList.contains('active'))
        listaLateral.classList.remove('active');
    })
}