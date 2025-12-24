import api from "../api-config";
import axios from "axios";
import { GenerateReportPayload } from "../../Types/commonTableTypes";

// Fetches the list of Standard Profile Field
export const fetchStandardProfileFieldApi = async (clientId: string, pageIndex: number, pageSize:number ) => {
  const response = await api.get(`/Report/standardprofilefield?ClientId=${clientId}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  return response.data;
};

// Fetches the Learner progress report data
export const generateReportApi = async (payload: GenerateReportPayload) => {
    const response = await api.post('/Report/userbyactivitystatus', payload); 
    return response.data;
};
