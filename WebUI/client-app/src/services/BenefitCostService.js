import httpClient from "./HttpClient";
import * as ToastService from "./ToastService";

export const getBenefitCost = (employeeId) => {
  return httpClient.get(`benefitcost?employeeId=${employeeId}`)
    .then(resp => resp.data)
    .catch(error => ToastService.showErrorToast("Error: couldn't fetch employee benefit cost."));
}