﻿
const _modeloLibro = {
    id_libro: 0,
    descripcion: "",
    asignatura: 0,
    stock: 0,
}

function MostrarLibros() {

    fetch("/Libro/listarLibros")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaLibros tbody").html("");

                console.log(libro)
                responseJson.forEach((libro) => {
                    $("#tablaLibros tbody").append(
                        $("<tr>").append(
                            $("<td>").text(libro.id_libro),
                            $("<td>").text(libro.descripcion),
                            $("<td>").text(libro.asignatura.descripcion),
                            $("<td>").text(libro.stock),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-libro").text("Editar").data("dataLibro", libro),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-libro").text("Eliminar").data("dataLibro", libro),
                            )
                        )
                    )
                })

            }


        })


}


document.addEventListener("DOMContentLoaded", function () {

    MostrarEmpleados();

    fetch("/Home/listaDepartamentos")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {

            if (responseJson.length > 0) {
                responseJson.forEach((item) => {

                    $("#cboDepartamento").append(
                        $("<option>").val(item.idDepartamento).text(item.nombre)
                    )

                })
            }

        })

    $("#txtFechaContrato").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true
    })


}, false)


function MostrarModal() {

    $("#txtNombreCompleto").val(_modeloEmpleado.nombreCompleto);
    $("#cboDepartamento").val(_modeloEmpleado.idDepartamento == 0 ? $("#cboDepartamento option:first").val() : _modeloEmpleado.idDepartamento)
    $("#txtSueldo").val(_modeloEmpleado.sueldo);
    $("#txtFechaContrato").val(_modeloEmpleado.fechaContrato)


    $("#modalEmpleado").modal("show");

}

$(document).on("click", ".boton-nuevo-empleado", function () {

    _modeloEmpleado.idEmpleado = 0;
    _modeloEmpleado.nombreCompleto = "";
    _modeloEmpleado.idDepartamento = 0;
    _modeloEmpleado.sueldo = 0;
    _modeloEmpleado.fechaContrato = "";

    MostrarModal();

})

$(document).on("click", ".boton-editar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");


    _modeloEmpleado.idEmpleado = _empleado.idEmpleado;
    _modeloEmpleado.nombreCompleto = _empleado.nombreCompleto;
    _modeloEmpleado.idDepartamento = _empleado.refDepartamento.idDepartamento;
    _modeloEmpleado.sueldo = _empleado.sueldo;
    _modeloEmpleado.fechaContrato = _empleado.fechaContrato;

    MostrarModal();

})

$(document).on("click", ".boton-guardar-cambios-empleado", function () {

    const modelo = {
        idEmpleado: _modeloEmpleado.idEmpleado,
        nombreCompleto: $("#txtNombreCompleto").val(),
        refDepartamento: {
            idDepartamento: $("#cboDepartamento").val()
        },
        sueldo: $("#txtSueldo").val(),
        fechaContrato: $("#txtFechaContrato").val()
    }


    if (_modeloEmpleado.idEmpleado == 0) {

        fetch("/Home/guardarEmpleado", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpleado").modal("hide");
                    Swal.fire("Listo!", "Empleado fue creado", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Home/editarEmpleado", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpleado").modal("hide");
                    Swal.fire("Listo!", "Empleado fue actualizado", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })

    }


})


$(document).on("click", ".boton-eliminar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar empleado "${_empleado.nombreCompleto}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Home/eliminarEmpleado?idEmpleado=${_empleado.idEmpleado}`, {
                method: "DELETE"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Empleado fue elminado", "success");
                        MostrarEmpleados();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })

        }



    })

})