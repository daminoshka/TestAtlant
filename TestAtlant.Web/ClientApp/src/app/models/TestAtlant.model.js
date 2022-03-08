"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.InsuranceContractForm = void 0;
var forms_1 = require("@angular/forms");
var InsuranceContractForm = /** @class */ (function () {
    function InsuranceContractForm() {
        this.formBuilder = new forms_1.FormBuilder();
        this.createForm();
    }
    InsuranceContractForm.prototype.ngOnInit = function () {
        this.createForm();
    };
    InsuranceContractForm.prototype.createForm = function () {
        this.model = new forms_1.FormGroup({
            Id: new forms_1.FormControl(0),
            NumberContract: new forms_1.FormControl("", forms_1.Validators.required),
            DateContract: new forms_1.FormControl(new Date(), forms_1.Validators.required),
            DateClose: new forms_1.FormControl(new Date(), forms_1.Validators.required),
            DateEnd: new forms_1.FormControl(new Date(), forms_1.Validators.required),
            Employee: new forms_1.FormGroup({
                Id: new forms_1.FormControl(0),
                PersonnelNumber: new forms_1.FormControl("", forms_1.Validators.required),
                Surname: new forms_1.FormControl("", forms_1.Validators.required),
                Name: new forms_1.FormControl("", forms_1.Validators.required),
                Patronymic: new forms_1.FormControl("", forms_1.Validators.required),
                Email: new forms_1.FormControl("", [forms_1.Validators.required, forms_1.Validators.email])
            }),
            Contractor: new forms_1.FormGroup({
                Id: new forms_1.FormControl(0, forms_1.Validators.required),
                Name: new forms_1.FormControl("", forms_1.Validators.required),
                PhoneNumber: new forms_1.FormControl("", [
                    forms_1.Validators.required
                ]),
                PhoneNumberAlt: new forms_1.FormControl(""),
                Email: new forms_1.FormControl("", [forms_1.Validators.required, forms_1.Validators.email]),
                IdentifyDocumentType: new forms_1.FormGroup({
                    Id: new forms_1.FormControl(0),
                    Name: new forms_1.FormControl("", forms_1.Validators.required)
                })
            }),
            InsuranceObject: new forms_1.FormGroup({
                Id: new forms_1.FormControl(0),
                Name: new forms_1.FormControl("", forms_1.Validators.required)
            }),
            InsuranceFormType: new forms_1.FormGroup({
                Id: new forms_1.FormControl(0),
                Name: new forms_1.FormControl("", forms_1.Validators.required)
            }),
            InsuranceSubindustry: new forms_1.FormGroup({
                Id: new forms_1.FormControl(0),
                Name: new forms_1.FormControl("", forms_1.Validators.required)
            }),
            Status: new forms_1.FormGroup({
                Id: new forms_1.FormControl(0),
                Name: new forms_1.FormControl("", forms_1.Validators.required)
            })
        });
    };
    return InsuranceContractForm;
}());
exports.InsuranceContractForm = InsuranceContractForm;
//# sourceMappingURL=insurance-contract.model.js.map