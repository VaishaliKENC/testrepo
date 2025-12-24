export interface AssignmentCardProps {
  // id:string;
  imageSrc: string;
  title: string;
  courseType: string;
  assignedDate: string;
  expiryDate?: string;
  expiryAlertDate?:string;
  completionDate?:string;
  dueDate?:string;
  isDueDate?:boolean;
  expiringSoon?: boolean;
  description?:string;
  showProgress?:boolean;
  progressBar?:number;
  activityStatus?:string;
  viewResult?:boolean;
}