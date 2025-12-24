import axios from 'axios';
import store from '../redux/store';
import { logout } from '../redux/Slice/authSlice';
import api from './api-config';
import { SignupDataAdminApprove } from '../Types/authTypes';

// Authentication API object using the shared Axios instance
const authApi = {
  getClientIdByUrl: (payload: { clientAccessURL: string }) =>
    api.post('/ClientDAM/getclientidbyurl', payload),

  getClientConfigByClientIdApi: (clientId: string) =>
    api.get('/ClientDAM/getclientconfig?ClientId='+clientId),

  // login: (payload: { userNameAlias: string; userPassword: string; ClientId: string | null }) =>
  //   api.post('/LearnerDAM/login', payload),

  login: (payload: { userNameAlias: string; userPassword: string; ClientId: string | null }, forceLogin: boolean) =>
    api.post(`LearnerDAM/login?forceLogin=${forceLogin}`, payload),

  signup: (payload: { FirstName: string; LastName: string; EmailID: string; UserNameAlias: string; ClientId: string | null; }) =>
    api.post('/LearnerDAM/adduser', payload),

  signupAdminApprove: (payload: SignupDataAdminApprove) =>
    api.post('/LearnerDAM/signup', payload),

  forgotPassword: (payload: { UserNameAlias: string; ClientId: string | null }) =>
    api.post('/LearnerDAM/getuserbyaliasforgotpassword', payload),

  checkavailablity: (payload: { UserNameAlias: string; ClientId: string | null }) =>
    api.post('/LearnerDAM/checkuserbyalias', payload),

  logout: (payload: { token: string }) =>
    api.post("/LearnerDAM/Logout", payload),
};

export default authApi;
