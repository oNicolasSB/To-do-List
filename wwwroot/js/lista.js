let botaoAbrirLista = window.document.querySelector("#criarLista");
let modalCriarLista = window.document.querySelector("#criarListaModal");
let botaoAbrirTarefa = window.document.querySelector("#criarTarefa");
let modalCriarTarefa = window.document.querySelector("#criarTarefaModal");
let itensLista = window.document.querySelectorAll(".lista-item");

if(botaoAbrirLista != null) {
    botaoAbrirLista.addEventListener("click", () => {
        if (modalCriarLista.classList.contains("d-none")) {
            modalCriarLista.classList.remove("d-none");
        }
        let botaoFechar = modalCriarLista.querySelector(".fechar-modal");
        botaoFechar.addEventListener("click", () => {
            modalCriarLista.classList.add("d-none");
        });
    });
}
if(botaoAbrirTarefa != null) {
    botaoAbrirTarefa.addEventListener("click", () => {
        if (modalCriarTarefa.classList.contains("d-none")) {
            modalCriarTarefa.classList.remove("d-none");
        }
        let botaoFechar = modalCriarTarefa.querySelector(".fechar-modal");
        botaoFechar.addEventListener("click", () => {
            modalCriarTarefa.classList.add("d-none");
        });
    });
}

if(itensLista != null) {
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
        btnDeletar = i.querySelector(".botao-deletar");
        btnDeletar.addEventListener("click", () => {
            let modalDeletar = i.querySelector('.deletar-lista');
            if(modalDeletar.classList.contains("d-none")) {
                modalDeletar.classList.remove("d-none");
            }
            let botaoFechar = modalDeletar.querySelectorAll(".fechar-modal");
            botaoFechar.forEach(b => {
                b.addEventListener("click", () => {
                    modalDeletar.classList.add("d-none");
                })
            })
        })
    });
}

