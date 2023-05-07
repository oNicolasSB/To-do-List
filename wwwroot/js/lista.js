let botaoAbrir = window.document.querySelector("#criarLista");
let modal = window.document.querySelector("#criarListaModal");

botaoAbrir.addEventListener("click", () => {
    if (modal.classList.contains("d-none")) {
        modal.classList.remove("d-none");
    }
    botaoFechar = modal.querySelector(".fechar-modal");
    botaoFechar.addEventListener("click", () => {
        modal.classList.add("d-none");
    })
})