//var DsUser = new kendo.data.DataSource({
//    type: "aspnetmvc-ajax",
//    transport: {
//        read: {
//            async: false,
//            url: URLROOT + "FormationAdmin/ReadUser",
//            type: "POST"
//        }
//    },
//    schema: {
//        model: {
//            id: "id",
//        }
//    }
//});


/* Fin Abonnes */

var DsAbonnes = new kendo.data.DataSource({
    transport: {
        read: {
            url: URLROOT + "Abonne/ReadAbonne",
            type: "POST",
        }
    }
});

var selectedType = null;

var isAllType = false;

var DsType = new kendo.data.DataSource({
    type: "aspnetmvc-ajax",
    batch: true,
    transport: {
        read: {
            async: false,
            url: URLROOT + "TypeDepense/ReadType",
            type: "GET",
            data: function () {
                return {
                    type: selectedType,
                    isAllType: isAllType
                }
            }
        },
        update: {
            // async: false,
            url: URLROOT + "TypeDepense/UpdateType",
            type: "POST",

        },
        create: {
            // async: false,
            url: URLROOT + "TypeDepense/CreateType",
            type: "POST",
            data: function () {
                return {
                    type: selectedType
                }
            }
        },
        destroy: {
            url: URLROOT + "TypeDepense/DeleteType",
            type: "POST",
        }
    },
    schema: {
        model: {
            id: "id",
            fields: {
                id: { field: "id", type: "number" },
                lb: { field: "lb", type: "string" }

            }
        }
    },
    requestEnd: function (e) {
        if (e.type !== "read") {
            var message = "";
            for (var i = 0 ; i < e.response.length ; i++) {
                var m = e.response[i].message;
                if (m != null)
                    message += m + "\n";
            }
            if (message != "") {
                notification.show({ title: "", message: message }, "error");
                this.read();
            }
        }
    }
});

var etatDS = new kendo.data.DataSource({
    transport: {
        read: {
            async: false,
            url: URLROOT + "Orga/ReadEtatData",
            type: "POST",
            contentType: "application/json",
            dataType: "json"
        }
    },
    schema: {
        model: {
            id: "id"
        }
    }
});
//_________________________________________ DsPieces_J :

var DsPieces_J = new kendo.data.DataSource({
    transport: {
        read: {
            type: "POST",
            url: URLROOT + "Depense/GetPiecesJ",
        }
    },
    schema: {
        model: {
            id: "fileName",
            fields: {
                fileName: { type: "string" },
            }
        }
    }
});
//_________________________________________ DsForms :

//var DsForms = new kendo.data.DataSource({
//    type: "aspnetmvc-ajax",
//    transport: {
//        read: {
//            url: URLROOT + "Form/ReadForms",
//            type: "GET",
//        },
//        create: {
//            url: URLROOT + "Form/CreateForm",
//            type: "POST",
//        },
//        update: {
//            url: URLROOT + "Form/UpdateForm",
//            type: "POST",
//        },
//    },
//    batch: true,
//    schema: {
//        model: {
//            id: "id",
//            fields: {
//                id: { type: "number" },
//                title: { type: "string" },
//                desc: { type: "string" },
//                duration: { type: "number" }
//            }
//        }
//    }
//});
//________________________________________ refresheSolde :

function refresheSolde() {
    $.ajax({
        type: "POST",
        //url: URLROOT + "Forms/getSolde",
        url: URLROOT + "Forms/readForms?group=true&solde=true",
        data: { idAbonne: OnSelectAbonne },
        success: function (Res) {
            $("#lbSolde").text(Res);

        }
    });
}
//_________________________________________ DsDepense :

var DsDepense = new kendo.data.DataSource({
    type: "aspnetmvc-ajax",
    batch: true,
    transport: {
        read: {
            url: URLROOT + "Depense/ReadDepense",
            type: "GET",
        },
        update: {
            url: URLROOT + "Depense/UpdateDepense",
            type: "POST",
        },
        destroy: {
            url: URLROOT + "Depense/DestroyDepense",
            type: "POST",
        },
        create: {
            url: URLROOT + "Depense/CreateDepense",
            type: "POST",
            data: function () {
                return {
                    idUser: drpUser.value(),
                }
            }
        }
    },
    schema: {
        model: {
            id: "id",
            fields: {
                id: { type: "number", editable: false },
                idUser: { type: "string", editable: false },
                idType: { type: "number" },
                quantite: { type: "number" },
                description: { type: "string", editable: false },
                montant: { type: "number" },
                date: { type: "date", editable: false },
                hasFiles: { type: "boolean" }
            }
        }
    },
    group: {
        field: "idUser",
        aggregates: [
                { field: "montant", aggregate: "sum" },
        ],
    }
});
// 

//var DsDepense = new kendo.data.DataSource({
//    type: "aspnetmvc-ajax",
//    batch: true,
//    transport: {
//        read: {
//            url: URLROOT + "Depense/ReadDepense",
//            type: "GET",
//        },
//        update: {
//            url: URLROOT + "Depense/UpdateDepense",
//            type: "POST",
//        },
//        destroy: {
//            url: URLROOT + "Depense/DestroyDepense",
//            type: "POST",
//        },
//        create: {
//            url: URLROOT + "Depense/CreateDepense",
//            type: "POST",
//        },
//    },
//    requestStart: function () {
//    },
//    requestEnd: function () {
//    },
//    schema: {
//        model: {
//            id: "id",
//            fields: {
//                id: { type: "number" },
//                idUser: { type: "string" },
//                idType: { type: "number" },
//                description: { type: "string" },
//                montant: { type: "number", },
//                date: { type: "date" },
//            }
//        }
//    },
//    pageSize: 10,
//    group: {
//        field: "idType",
//        aggregates: [
//            { field: "montant", aggregate: "sum" },
//        ],
//    },
//    aggregate: [
//         { field: "idUser", aggregate: "count" },
//           { field: "idType", aggregate: "count" },
//        { field: "montant", aggregate: "sum" },
//    ],
//});
//_________________________________________ DsUtilisateur :

var DsUtilisateur = new kendo.data.DataSource({
    type: "aspnetmvc-ajax",
    transport: {
        read: {
            async: false,
            url: URLROOT + "User/ReadUser",
            type: "GET",
            data: OnSelectAbonne
        },
        create: {
            url: URLROOT + "User/CreateUser",
            type: "POST",
            data: OnSelectAbonne
        },
        update: {
            url: URLROOT + "User/UpdateUser",
            type: "POST"
        }
    },
    schema: {
        model: {
            id: "id",
            fields: {
                id: { type: "string" },
                name: { type: "string" },
                civilite: { type: "number", defaultValue: 1 },
                email: { type: "string" },
                password: { type: "string" },
                tel: { type: "string" },
            }
        }
    }
});

//DsUtilisateur.read();

//_________________________________ DsCivilite :

var DsCivilite = new kendo.data.DataSource({
    type: "aspnetmvc-ajax",
    transport: {
        read: {
            async: false,
            url: URLROOT + "User/ReadCivilite",
            type: "GET",
        }
    },
    schema: {
        model: {
            id: "id",
            fields: {
                id: { type: "number" },
                lb: { type: "string" },
            }
        }
    }
});

DsCivilite.read();

var DsUsersBySup = new kendo.data.DataSource({
    type: "aspnetmvc-ajax",
    transport: {
        read: {
            async: false,
            url: URLROOT + "User/ReadUsersBySup",
            type: "GET",
            data: OnSelectAbonne
        }
    },
    schema: {
        model: {
            id: "id",
            fields: {
                id: { type: "string" },
                name: { type: "string" },
                email: { type: "string" },
                password: { type: "string" },
                tel: { type: "string" },
                formations: { editable: false },
                projets: { editable: false }
            }
        }
    }
});

//DsUsersBySup.read();

var DsUsersParent = new kendo.data.DataSource({
    type: "aspnetmvc-ajax",
    transport: {
        read: {
            async: false,
            url: URLROOT + "User/ReadUsersParents",
            type: "GET",
            data: OnSelectAbonne
        }
    },
    schema: {
        model: {
            id: "id",
            fields: {
                id: { type: "string" },
                name: { type: "string" },
                email: { type: "string" },
                password: { type: "string" },
                tel: { type: "string" },
                formations: { editable: false },
                projets: { editable: false }
            }
        }
    }
});

var selectedAbonne = null;

var drpAbonne = null;

function OnSelectAbonne() {
    return {
        idAbonne: selectedAbonne
    }
}

$(function () {
    drpAbonne = $("#drpAbonne").kendoDropDownList({
        dataSource: DsAbonnes,
        dataTextField: "lb",
        dataValueField: "id",
        change: function () {
            selectedAbonne = this.value();
        }
    }).data("kendoDropDownList");

    if (typeof ChangeAbonne !== "undefined") {
        drpAbonne.bind("change", ChangeAbonne);
    }
});

/* Fin Abonnes */

function openMail(idMail) {
    var dialog = $("<div id='wndVisualiserMailExt'><iframe class='k-content-frame' style='height: 842px;'></iframe></div>").kendoWindow({
        width: "845px",
        title: "Visualiser Mail",
        close: function () {
            this.destroy();
        },
        content: URLROOTCRM + "CategorieMail/VisualiserMailExt?idMail=" + idMail,
        iframe: true,
        modal: true,
        statut: null,
    }).data("kendoWindow");

    dialog.center().open();
}

function showLoad() {
    kendo.ui.progress($("body"), true);
}

function hideLoad() {
    kendo.ui.progress($("body"), false);
}

//*******************************************************************************************************************************************
//****** data source for CmbIconFieldGroup**********

var DscmbDataIconFieldGroup = new kendo.data.DataSource({
        transport: {
            read: {
                url: URLROOT + "Form/GET_DataIconFieldGroup",
                type:"GET"
            }
        },
        group: { field: "Gp" }
});

//****** data source for DscmbDataField********
var DscmbDataField = new kendo.data.DataSource({
    transport: {
        read: {
            url: URLROOT + "Form/GET_DataField",
            type: "GET"
        }
    }
});

//********************** data source for listView form group**********************************************

var DsListViewFormGroup = new kendo.data.DataSource({
    transport: {
        read: {
            url: URLROOT + "FormsManager/GetFormGroups",
            type: "GET"
        },
        update: {
            url: URLROOT + "FormsManager/UpdateGroup",
            type: "POST"
        },
        destroy: {
            url: URLROOT + "FormsManager/DeleteGroup",
            type: "POST"
        },
        create: {
            url: URLROOT + "FormsManager/CreateGroup",
            type: "POST"
        },

    },
    pageSize:5,
    schema: {
        model: {
            id: "id",
            fields: {
                id: { type: "number", editable: false },
                name: { type: "string", editable: true },
                desc: { type: "string", editable: true }
            }
        }

    }

});
//*************************************************Ds for listview form**********************************************
var DsForms = new kendo.data.DataSource({
    transport: {
        read: {
            url: URLROOT + "FormsManager/GetForms",
            type: "GET",
        },
        create: {
            url: URLROOT + "FormsManager/CreateForm",
            type: "POST",
        },
        update: {
            url: URLROOT + "FormsManager/UpdateForm",
            type: "POST",
        },
        destroy: {
            url: URLROOT + "FormsManager/DeleteForm",
            type: "POST"
        },
    },
    pageSize:5,
    schema: {
        model: {
            id: "id",
            fields: {
                id: { type: "number", editable: false },
                title: { type: "string" },
                desc: { type: "string" },
                duration: { type: "number" },
                IdGroup: { type: "number" },
                groupName: { type: "string" }
            }
        }
    }
    
});
//**************************************Ds for list view  section***************************************** 

//var DsListViewSection = kendo.observable({
//    isVisible: true,
//    onSave: function (e) {
//        console.log("event :: save(" + kendo.stringify(e.model, null, 4) + ")");
//    },
//    sections: new kendo.data.DataSource({
//        schema: {
//            model: {
//                id: "ID",
//            }
//        },
//        transport: {
//            read: {
//                url: URLROOT + "FormsManager/GetSection",
//                type: "GET"
//            },
//            create: {
//                url: URLROOT + "FormsManager/CreateSection",
//                type: "POST"
//            },
//            update: {
//                url: URLROOT + "FormsManager/UpdateSection",
//                type: "POST"
//            },
//            destroy: {
//                url: URLROOT + "FormsManager/DeleteSection",
//                type: "POST"
//            },
//        }
//    })
//});




////*******************old******************************
var DsListViewSection = new kendo.data.DataSource({
    transport: {
        read: {
            url: URLROOT + "FormsManager/GetSection",
            type: "GET"
        },
        create: {
                url: URLROOT + "FormsManager/CreateSection",
                type:"POST"
        },
        update: {
            url: URLROOT + "FormsManager/UpdateSection",
            type:"POST"
        },
        destroy: {
            url: "FormsManager/DeleteSection",
            type:"POST"
        }
    },
    schema: {
        model: {
            id: "ID",
            fields: {
                ID: { type: "number", editable: false },
                Title: { type: "string" },
                Desc: { type: "string" },
                FormId: { type: "number" },
                fTitle: { type: "string" }
            }
        }
    }

});

//**************************************Ds for list view Question***************************************** 
//var DsListViewQuestion = new kendo.data.DataSource({
//    transport: {
//        read: {
//            url: URLROOT + "FormsManager/GetPreparedQuestions",
//            type: "GET"
//        },
//        create: {
//            url: URLROOT + "FormsManager/CreateQuestion",
//            type: "POST"
//        },
//        update: {
//            url: URLROOT + "FormsManager/UpdateQuestion",
//            type: "POST"
//        },
//        destroy: {
//            url: "FormsManager/DeleteQuestion",
//            type: "POST"
//        }
//    },
//    schema: {
//        model: {
//            id: "ID",
//            fields: {
//                ID: { type: "number", editable: false },
//                Numbering: { type: "number" },
//                Desc: { type: "string" },
//                SectionID: { type: "number" },
//                SectionTitle: { type: "string" }
//            }
//        }
//    }



//});
//**************************************Ds for list view PreparedQuestion***************************************** 
var DsListViewPrQuestion = new kendo.data.DataSource({
    transport: {
        read: {
            url: URLROOT + "FormsManager/GetPreparedQuestions",
            type: "GET"
        },
        create: {
            url: URLROOT + "FormsManager/CreatePreparedQuestion",
            type: "POST"
        },
        update: {
            url: URLROOT + "FormsManager/UpdatePreparedQuestion",
            type: "POST"
        },
        destroy: {
            url: "FormsManager/DeletePreparedQuestion",
            type: "POST"
        }
    },
    schema: {
        model: {
            id: "ID",
            fields: {
                ID: { type: "number", editable: false },
                Desc: { type: "string" },
               
            }
        }
    }
});

//**************************************Ds for pr group filed****************************************************
var DsListViewGroupField = new kendo.data.DataSource({
    transport: {
        read: {
            url: URLROOT + "FormsManager/GetPreparedFieldGroups",
            type: "GET"
        },
        update: {
            url: URLROOT + "FormsManager/UpdatePreparedFieldGroup",
            type: "POST"
        },
        destroy: {
            url: URLROOT + "FormsManager/DeletePreparedFieldGroup",
            type: "POST"
        },
        create: {
            url: URLROOT + "FormsManager/CreatePreparedFieldGroup",
            type: "POST"
        },

    },
    pageSize: 5,
    schema: {
        model: {
            id: "ID",
            fields: {
                ID: { type: "number", editable: false },
                Name: { type: "string", editable: true },
                QuestionID: { type: "number", editable: true }
            }
        }

    }

});
//**************************************Ds for pr filed****************************************************
var DsListViewField = new kendo.data.DataSource({
    transport: {
        read: {
            url: URLROOT + "FormsManager/GetPreparedField",
            type: "GET"
        },
        update: {
            url: URLROOT + "FormsManager/UpdatePreparedField",
            type: "POST"
        },
        destroy: {
            url: URLROOT + "FormsManager/DeletePreparedField",
            type: "POST"
        },
        create: {
            url: URLROOT + "FormsManager/CreatePreparedField",
            type: "POST"
        },

    },
    pageSize: 5,
    schema: {
        model: {
            id: "ID",
            fields: {
                ID: { type: "number", editable: false },
                Name: { type: "string", editable: true },
                PreparedGroupFieldID: { type:"number", editable: true }
            }
        }

    }

});