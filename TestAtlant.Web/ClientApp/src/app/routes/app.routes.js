"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.appRoutes = void 0;
var counter_component_1 = require("../components/counter/counter.component");
var home_component_1 = require("../components/home/home.component");
var insurance_contracts_component_1 = require("../components/details/detail.component");
var insurance_contracts_routes_1 = require("./details.routes");
exports.appRoutes = [
    { path: '', component: home_component_1.HomeComponent, pathMatch: 'full' },
    { path: 'counter', component: counter_component_1.CounterComponent },
    { path: 'details', component: insurance_contracts_component_1.InsuranceContractsComponent, children: insurance_contracts_routes_1.insuranceContractRoutes },
];
//# sourceMappingURL=app.routes.js.map
