//---------- VARIABLES ------------------------------
const Categorias = ['Rifles','Arcos','Francotiradores','Lanzadores','Escopetas','Secundarias','Melee','ArchGun','Compañero'];
let Stats; let loader;
let armas = [''];
let Prefi, Nucl, Sufi;
let Riven = {
    Propietario: "",
    Arma: "",
    Nombre: "",
    Polaridad: 0,
    RangoMaestria: 0,
    RangoMod: 0,
    Rolls: 0,
    Buff1N: "",
    Buff1V: 0,
    Buff2N: "",
    Buff2V: null,
    Buff3N: "",
    Buff3V: null,
    DeBuffN: "",
    DeBuffV: null
}
//---------- INICIO ------------------------------
function Load(){
    //Cargamos json desde local
    LoadJSONStats()
    let selector = document.querySelector('#TypeWeapon'); //Seleccionamos selector de categoría
    Categorias.forEach( cat => {
        let opcion = document.createElement('option'); //Creamos option de select
        opcion.textContent = cat; //Añadimos categoria a texto
        opcion.value = cat; //Añadimos categoria a valor
        selector.appendChild(opcion); //Añadimos option a select
    });
    ChangesLoader(); //Cargamos eventos
}
//---------- CHANGERS ------------------------------
function ChangesLoader(){
    document.querySelector('#TypeWeapon').addEventListener('change',WeaponSelect); //Evento a selector de arma
    document.querySelector('#MaestryValue').addEventListener('change',ValidMaestry); //Evento a maestria
    document.querySelector('#ModRankValue').addEventListener('change',ValidModRank); //Evento a rango de mod
    document.querySelector('#RollsValue').addEventListener('change',ValidRollsValue); //Evento a rolls
    document.querySelector('#submit button').addEventListener('click',AddRiven); //Evento a añadir riven
}
function ChangeTypeWeapon(){

}
function ActivarZona(a){
    let seleccion = document.querySelector('#sb'+a); //Seleccionamos selector de buff
    let unidades = document.querySelector('#ud'+a); //Seleccionamos unidades de buff
    let decimas = document.querySelector('#dc'+a); //seleccionamos decimales de buff
    let caja = document.querySelector('#ch'+a); //seleccionamos check de que está activo
    if (caja.checked == true){ //¿La caja está activada?
        seleccion.removeAttribute('disabled'); // Selección no está desactivada
        unidades.removeAttribute('disabled'); // Unidades no están desactivadas
        decimas.removeAttribute('disabled'); // Decimales no están desactivadas
    }
    if (caja.checked == false){ 
        seleccion.setAttribute('disabled','true'); // Selección está desactivada
        unidades.setAttribute('disabled','true'); // Unidades están desactivadas
        decimas.setAttribute('disabled','true'); // Decimales están desactivadas
    }
    document.querySelector('#ud'+a).addEventListener('change',ValidUnitValue(a)); //Evento a unidades buff1
    document.querySelector('#dc'+a).addEventListener('change',ValidDecimalValue(a)); //Evento a decimales buff1
    StadisticsLoader(seleccion); //Cargamos las estadisticas de esa categoria
}
function DesactivarZona(){
    for(let a = 1; a <= 4; a++){
        let seleccion = document.querySelector('#sb'+a);
        let caja = document.querySelector('#ch'+a); 
        let unidades = document.querySelector('#ud'+a);
        let decimas = document.querySelector('#dc'+a);
        seleccion.setAttribute('disabled','true');
        unidades.setAttribute('disabled','true');
        decimas.setAttribute('disabled','true');
        caja.checked = false;
    }
}
async function WeaponSelectLoader() {
    let selectName = document.querySelector('#Weapon'); // Seleccionamos select arma
    while (selectName.firstElementChild){
        selectName.removeChild(selectName.firstElementChild); //limpiamos
    }
    armas.forEach( wpn =>{ 
        let opcion = document.createElement('option'); //Creamos option
        opcion.textContent = wpn; //Añadimos arma a texto
        opcion.value = wpn; // Añadimos arma a valor
        selectName.appendChild(opcion); // Añadimos option a select de armas
    });
} 
function StadisticsLoader(selector){
    while (selector.firstElementChild){ 
        selector.removeChild(selector.firstElementChild); // Seleccionamos primer elemento y lo eliminamos
    }
    for (let i in Stats){
        let melee; // Creamos bool melee
        if (document.querySelector('#TypeWeapon').value == 'Melee'){ //¿Melee es verdadero?
            melee = true; // Melee es verdadero
        }
        const Arr = Stats[i]; // Metemos stat en el array
        if (melee){
            if (Arr[0].use == 0 || Arr[0].use == 2){ // Si stat es 0 (global) o 2 (solo melee)
                let opcion = document.createElement('option'); // Creamos option
                opcion.textContent = Arr[0].Nombre; // Añadimos stat a texto
                opcion.value = i; // Añadimos stat a valor
                selector.appendChild(opcion); // Añadimos option a select de buff
            }
        }
        else{
            if (Arr[0].use == 0 || Arr[0].use == 1){ // Si stat es 0 (global) o 1 (solo armas de fuego) 
                let opcion = document.createElement('option'); // Creamos option
                opcion.textContent = Arr[0].Nombre; // Añadimos stat a texto
                opcion.value = i; // Añadimos stat a valor
                selector.appendChild(opcion); // Añadimos option a select de buff
            }
        }
    }
}
//---------- LOADERS ------------------------------
function LoadJSONStats(){
    fetch("../src/buff.json")
    .then(response => response.json())
    .then(data => {Stats = data;})
    .catch(error => console.error('Error al cargar el archivo JSON:', error));
}
function LoaderArray(tipo){
    return new Promise((resolve, reject) => {
        fetch(tipo)
        .then(response => response.text())
        .then(data => {
            const dataArray = data.split(';');
            resolve(dataArray);
        })
        .catch(error => {
            console.error('Error al cargar archivo: ', error);
            reject(error);
        });
    });
}
function WeaponSelect(){
    let selectTipo = document.querySelector('#TypeWeapon').value; // Obtiene valor de categoria
    if (selectTipo != 'pred'){ // Si el seleccionado no es opción predeterminada
        loader = LoaderArray('../src/weapons/'+selectTipo+'.txt'); // En loader meto todas las armas
        loader.then( a => {
            armas = a; // Añadimos el arma al selector
            WeaponSelectLoader(); 
        });
    }
    else {
        while (document.querySelector('#Weapon').firstElementChild){
            document.querySelector('#Weapon')
            .removeChild(document.querySelector('#Weapon')
            .firstElementChild); // Elimino todos los hijos
        }
    }
    DesactivarZona(); // Al cambiar de arma desactivamos las zonas
}
//---------- VALIDACIONES ------------------------------
function MarkValid(campo,valid) {
    if (valid){ //¿El campo es válido?
        campo.style.border = '2px solid green'; 
        // Marcamos el borde como verde, de que está correcto ese campo
    }
    else {
        campo.style.border = '2px solid red';
        // Marcamos el borde como rojo, de que está incorrecto ese campo
    }
}
function ValidUnitValue(i){
    let valor = document.querySelector('#ud'+i) //Seleccionamos unidades de zona marcada    
    if (-999 <= valor.value && valor.value <= 999){ //¿Está en los valores?
        MarkValid(valor, true); // Marcamos en verde
    }
    else {
        MarkValid(valor, false); // Marcamos en rojo
    }
}
function ValidDecimalValue(i){
    let valor = document.querySelector('#dc'+i); //Seleccionamos decimales de zona marcada
    if (0 <= valor.value && valor.value <= 9){ //¿Está en los valores?
        MarkValid(valor, true); // Marcamos en verde
    }
    else {
        MarkValid(valor, false); // Marcamos en rojo
    }
}
function ValidMaestry(){
    let valor = document.querySelector('#MaestryValue'); // Seleccionamos maestría
    if (8 <= valor.value && valor.value <= 32){ //¿Está en los valores?
        MarkValid(valor, true); // Marcamos en verde
    }
    else {
        MarkValid(valor, false); // Marcamos en rojo
    }
}
function ValidModRank(){
    let valor = document.querySelector('#ModRankValue'); // Seleccionamos rango de mod
    if (1 <= valor.value && valor.value <= 8){ //¿Está en los valores?
        MarkValid(valor, true); // Marcamos en verde
    }
    else {
        MarkValid(valor, false); // Marcamos en rojo
    }
}
function ValidRollsValue(){
    let valor = document.querySelector('#RollsValue'); // Seleccionamos rolls
    if (0 <= valor.value && valor.value <= 999){ //¿Está en los valores?
        MarkValid(valor, true); // Marcamos en verde
    }
    else {
        MarkValid(valor, false); // Marcamos en rojo
    }
}
//---------- VALIDACIONES FINALES ------------------------------
function AddRiven(){
    let nbuff = 0;
    let b = true; // Booleano es verdadero
    let Name = []; // Nombre es array vacío
    Riven.Arma = document.querySelector("#Weapon").textContent; // Nombre de riven es texto de arma
    Riven.Polaridad = document.querySelector("#Polaridad").value; // Polaridad de riven es valor de polaridad
    if (8 <= document.querySelector("#MaestryValue").value 
        && document.querySelector("#MaestryValue").value <= 34){ //¿Es maestría válida?
        Riven.RangoMaestria = document.querySelector("#MaestryValue").value;
        // Maestría de riven es valor de maestría
    }
    else {
        b = false; // Booleano es falso
    }
    if (0 <= document.querySelector("#ModRankValue").value 
        && document.querySelector("#ModRankValue").value <= 8){ //¿Es rango válido?
        Riven.RangoMod = document.querySelector("#ModRankValue").value;
        // Rango de riven es valor de rango
    }
    else {
        b = false; // Booleano es falso
    }
    Riven.Propietario = document.querySelector('#user').textContent;
    if (0 <= document.querySelector('#RollsValue').value 
        && document.querySelector('#RollsValue').value <= 999){
        Riven.Rolls = document.querySelector('#RollsValue').value;
        // Rolls de riven es valor de rolls
    }
    else {
        b = false; // Booleano es falso
    }
    let caja1 = document.querySelector('#ch1');
    if (caja1.checked == true){ //¿Caja 1 está activada?
        if (FinalValidUnitValue(1)){ 
            if (FinalValidDecimalValue(1)){
                Riven.Buff1N = document.querySelector('#sb1').value;
                Name[Name.length] = document.querySelector('#sb1').value;
                let a = document.querySelector('#ud1').value + (document.querySelector('#dc1').value/10);
                Riven.Buff1V = a;
                nbuff ++;
            }
            else{
                b = false;
            }
        }
        else{
            b = false;
        }
    }
    let caja2 = document.querySelector('#ch2');
    if (caja2.checked == true){
        if (FinalValidUnitValue(2)){
            if (FinalValidDecimalValue(2)){
                Riven.Buff2N = document.querySelector('#sb2').value;
                Name[Name.length] = document.querySelector('#sb2').value;
                let a = document.querySelector('#ud2').value + (document.querySelector('#dc2').value/10);
                Riven.Buff2V = a;
                nbuff ++;
            }
            else{
                b = false;
            }
        }
        else{
            b = false;
        }
    }
    let caja3 = document.querySelector('#ch3');
    if (caja3.checked == true){
        if (FinalValidUnitValue(3)){
            if (FinalValidDecimalValue(3)){
                Riven.Buff3N = document.querySelector('#sb3').value;
                Name[Name.length] = document.querySelector('#sb3').value;
                let a = document.querySelector('#ud3').value + (document.querySelector('#dc3').value/10);
                Riven.Buff3V = a;
                nbuff ++;
            }
            else{
                i = -1000;
            }
        }
        else{
            i = -1000;
        }
    }
    let caja4 = document.querySelector('#ch4');
    if (caja4.checked == true){
        if (FinalValidUnitValue(4)){
            if (FinalValidDecimalValue(4)){
                Riven.DeBuffN = document.querySelector('#sb4').value;
                Name[Name.length] = document.querySelector('#sb4').value;
                let a = document.querySelector('#ud4').value + (document.querySelector('#dc4').value/10);
                Riven.DeBuffV = a;
                nbuff ++;
            }
            else{
                b = false;
            }
        }
        else{
            b = false;
        }
    }
    if (b){
        if (/*Name.length > 1*/ nbuff > 1){
            //si es menor no es válido y si es mayor mirar que nombre tiene
            //1 es pre,2 es nucleo y 3 es su
            alert('funciona');
        }
    }
    else{
        alert('Alguno de los campos es inválido')
    }
}
function FinalValidUnitValue(i){
    let valor = document.querySelector('#ud'+'1')
    if (-999 <= valor.value && valor.value <= 999){
        return true;
    }
    else {
        return false;
    }
}
function FinalValidDecimalValue(i){
    let valor = document.querySelector('#dc'+'1');
    if (1 <= valor.value && valor.value <= 9){
        return true;
    }
    else {
        return false;
    }
}
function Test(){console.log('Test')}