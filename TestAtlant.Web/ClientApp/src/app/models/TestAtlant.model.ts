import { OnInit } from "@angular/core";
import { FormBuilder, Validators, FormControl, FormGroup } from "@angular/forms";

export class DetailForm implements OnInit {

  public model: FormGroup = this.createForm();
  public formBuilder: FormBuilder = new FormBuilder();

  DetailForm() {
    this.createForm();
  }

  ngOnInit() {
    this.createForm();
  }

  createForm(): FormGroup {
    return new FormGroup({
      Id: new FormControl(0),
      NomCode: new FormControl("", Validators.required),//
      Name: new FormControl("", Validators.required),//
      Amount: new FormControl(0, Validators.required),//

      Storekeeper: new FormGroup({
        Id: new FormControl(0),
        Name: new FormControl("", Validators.required),
        EmployeeNumber: new FormControl("", Validators.required)
      })
    });
  }
}

/**
* Кладовщик
*/
export interface Storekeeper {
  Id: number;
  Name: string;
  Firstname: string;
  Lastname: string;
  Surname: string;
  EmployeeNumber: string;
}
