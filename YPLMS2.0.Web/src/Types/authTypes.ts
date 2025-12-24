// Types specific to authentication

  export interface User {
    id: string;
    name: string;
    email: string;
    token: string;
    clientId: string;
    learner?: {
      id: string;
      clientId: string;
      userAdminRole?: { roleId: string }[];    
    };
  }

  export interface AuthState {
    user: User | null;
    loading: boolean;
    error: string | null;
    isAuthenticated: boolean;
    success: boolean;
    successMessage: string | null;  
    token: string | null;
    clientId: string | null; 
    id: string | null; 
    roleId: number | null;
    userTypeId: string | null;
    fetchClientURL?: string ;
    firstName:string;
    lastName:string; 
    serverUrl:string;
    isSelfRegistration?: boolean;
    isSelfRegistrationAdminApproval?: boolean;
    isPassCodeBased?: boolean;
    isEmailDomainBased?: boolean;
  }

export interface LoginPayload { 
  userNameAlias:string;
  userPassword:string;
  ClientId:string | null;
  isActive: boolean
}
export interface SignupData {
  FirstName: string;
  LastName: string;
  EmailID: string;
  UserNameAlias: string; 
  ClientId:string | null;
}

export interface SignupDataAdminApprove {
  firstName: string;
  lastName: string;
  emailId: string;
  loginId: string; 
  clientId:string | null;
  systemuserGUID: string | null;
  country: string;
  organization: string;
  department: string;
  role: string;
}

export interface ForgotPasswordPayload { 
  userNameAlias:string;
  ClientId:string | null;
}
export interface fetchClientUrlPayload { 
  clientAccessURL:string;
}
export interface logOutPayload { 
  token:string;
}