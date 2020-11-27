var selectedRow = null;

function onFormSubmit() {

    var formData = leDados();
    if (selectedRow == null)
        inseriDados(formData);
    else
        atualizaDados(formData);
    limpaDados();
}


function validaDados() {

    $("#meuForm").validate({
        rules: {
            txtNome: {
                required: true,                
                maxlength: 50,
                minlength: 3


            },
            txtData: {
                
                required: true,
                dateBR: true,
                minlength: 10,
                maxlength: 10

            },
            Linguagem: { 
                required: true 
            },

            opcao: 
                {
                    required: function (elem) {
                        
                        return $('input[name=opcao]:checked').val() == undefined;
                    }
                },
        
            estado: {
                required: function (elem) {
                    return $("#estado").val() != '';
                }
            },
            txtMensagem: {
                required: true,
                minlength: 5,
                maxlength: 500
            }

        },
        messages:{
            txtNome:{ 
                required: "Digite o nome",                              
                maxlength: "Insira no maximo de 50 caracteres",
                minlength: "Insira no minino 3 caracteres"              
        },
        txtData: {
                
            required: "Informe uma data válida" ,
            dateBR:  "Informe uma data válida",
            

        },
        linguagem: { 
            required: "Selecione a Linguagem"
        },

        opcao: 
            {
                required: "Informe o sexo"
            },
    
        estado: {
            required: "Selecione o estado"
            
        },
        txtMensagem: {
            required: "Digitar uma mensagem",
            minlength: "Insira no minimo 5 caracteres",
            maxlength: "Insira no maximo 500 caracteres"
        }


        }

    });
}

$(document).ready(function () {


    $('#txtData').mask("99/99/9999");
    $("#txtNome").on("input", function(){
        var regexp = /[^a-zA-Z]/g;
        if(this.value.match(regexp)){
          $(this).val(this.value.replace(regexp,' '));
        }
      });
    
    
    validaDados();

    // $('#btnInserir').click(function () {
    //     $("#meuForm").valid();
    // });

    // $('#btnSalvar').click(function () {
    //     $("#meuForm").valid();

    // });

});


function leDados() {
    var formData = {};
    debugger;
    formData["nome"] = $('input[name=txtNome]').val();
    formData["dataNascimento"] = $("#txtData").val();
    formData["linguagem"] = $('input[name=linguagem]:checked').map(function () { return this.value; }).get().join(', ');
    formData["sexo"] = $('input[name=opcao]:checked').val();
    formData["estado"] = $("#estado").val();
    formData["mensagem"] = $("#txtMensagem").val();

    return formData;
   
}

function inseriDados(data) {

    if($("#meuForm").valid()){


    var pessoas = document.getElementById("tbPessoas");
    var linha = pessoas.insertRow(pessoas.length);


    cell1 = linha.insertCell(0);
    cell1.innerHTML = data.nome;
    cell2 = linha.insertCell(1);
    cell2.innerHTML = data.dataNascimento;
    cell3 = linha.insertCell(2);
    cell3.innerHTML = data.linguagem;
    cell4 = linha.insertCell(3);
    cell4.innerHTML = data.sexo;
    cell5 = linha.insertCell(4);
    cell5.innerHTML = data.estado;
    cell6 = linha.insertCell(5);
    cell6.innerHTML = data.mensagem;
    cell7 = linha.insertCell(6);
    cell7.innerHTML = '<a onClick="onEdit(this)">Editar</a><a onClick="onDelete(this)">Delete</a>';
}
}

function limpaDados() {
    $('#meuForm').each(function () {
        this.reset();
    });
    selectedRow = null;
}

function onEdit(td) {

    selectedRow = td.parentElement.parentElement;
    $("#txtNome").val(selectedRow.cells[0].innerHTML);
    $("#txtData").val(selectedRow.cells[1].innerHTML);

    var linguagensSelecionadas = selectedRow.cells[2].innerHTML.split(", ");

    $.each(linguagensSelecionadas, function (index, valor) {

        if (valor == "C#") {
            $("#csharp")[0].checked = true
        }
        if (valor == "Java") {
            $("#java")[0].checked = true
        }

        if (valor == "Python") {
            $("#python")[0].checked = true
        }

        if (valor == "PHP") {
            $("#php")[0].checked = true
        }

    });

    debugger;
    var sexoSelecionados = selectedRow.cells[3].innerHTML;
    for (var i = 0; i < 1; i++) {

        if (sexoSelecionados == "Feminino") {
            $("#Feminino")[0].checked = true
        }
        if (sexoSelecionados == "Masculino") {
            $("#Masculino")[0].checked = true
        }

    };

    $("#estado").val(selectedRow.cells[4].innerHTML);
    $("#txtMensagem").val(selectedRow.cells[5].innerHTML);
}
function atualizaDados(formData) {
    selectedRow.cells[0].innerHTML = formData.nome;
    selectedRow.cells[1].innerHTML = formData.dataNascimento;
    selectedRow.cells[2].innerHTML = formData.linguagem;
    selectedRow.cells[3].innerHTML = formData.sexo;
    selectedRow.cells[4].innerHTML = formData.estado;
    selectedRow.cells[5].innerHTML = formData.mensagem;
}

function onDelete(td) {
    if (confirm('Voce deseja deletar a linha ?')) {
        row = td.parentElement.parentElement;
        document.getElementById("tbPessoas").deleteRow(row.rowIndex);
        limpaDados();
    }
}

$.validator.addMethod("dateBR", function (value, element) {
    //contando chars    
    if (value.length != 10) return (this.optional(element) || false);
    // verificando data
    var data = value;
    var dia = data.substr(0, 2);
    var barra1 = data.substr(2, 1);
    var mes = data.substr(3, 2);
    var barra2 = data.substr(5, 1);
    var ano = data.substr(6, 4);
    if (data.length != 10 || barra1 != "/" || barra2 != "/" || isNaN(dia) || isNaN(mes) || isNaN(ano) || dia > 31 || mes > 12) return (this.optional(element) || false);
    if ((mes == 4 || mes == 6 || mes == 9 || mes == 11) && dia == 31) return (this.optional(element) || false);
    if (mes == 2 && (dia > 29 || (dia == 29 && ano % 4 != 0))) return (this.optional(element) || false);
    if (ano < 1900 ) return (this.optional(element) || false);
   
    return (this.optional(element) || true);
});
