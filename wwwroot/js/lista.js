let botaoAbrir = window.document.querySelector("#criarLista");
let modalCriar = window.document.querySelector("#criarListaModal");
let itensLista = window.document.querySelectorAll(".lista-item");

botaoAbrir.addEventListener("click", () => {
    if (modalCriar.classList.contains("d-none")) {
        modalCriar.classList.remove("d-none");
    }
    let botaoFechar = modalCriar.querySelector(".fechar-modal");
    botaoFechar.addEventListener("click", () => {
        modalCriar.classList.add("d-none");
    })
})

itensLista.forEach((i) => {
    btnEditar = i.querySelector(".botao-editar");
    btnEditar.addEventListener("click", () => {
        let modalEditar = i.querySelector('.editar-lista');
        if(modalEditar.classList.contains("d-none")) {
            modalEditar.classList.remove("d-none");
        }
        let botaoFechar = modalEditar.querySelector(".fechar-modal");
        botaoFechar.addEventListener("click", () => {
            modalEditar.classList.add("d-none");
        })
    })
});