export interface IMainPayloadLearningPath {
  clientId: string;
  keyWord?: null | string;
  createdByID?: null | string;
  LastModifiedById?: null | string;
  IsUsed?: boolean
  IsActive?: boolean
  listRange?: {
    pageIndex: number,
    pageSize: number,
    sortExp?: null | string,
  }

}

export interface ILearningPathFilterPayload {
  id: string;
  clientId: string;
  listRange: {
    pageIndex: number,
    pageSize: number,
    sortExpression: string,
  }
  keyWord: string,

}