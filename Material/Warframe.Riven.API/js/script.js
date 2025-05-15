function CargarVista(vista) {
    const mainContent = document.querySelector('main');
    fetch(vista).then(response => response.text())
    .then(data => {mainContent.innerHTML = data;})
    .catch(error => console.error('Error al cargar la vista:', error));
}