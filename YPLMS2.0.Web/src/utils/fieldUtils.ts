export const isFieldIncluded = (fields: any, fieldName: string) => {
    const fieldData = fields.find((field: any) => field.fieldName === fieldName);
    return fieldData ? fieldData.include : false;
}

export const isFieldAllowBlanks = (fields: any, fieldName: string) => {
    const fieldData = fields.find((field: any) => field.fieldName === fieldName);
    return fieldData ? fieldData.allowBlank : false;
}

export const isFieldIsDefault = (fields: any, fieldName: string) => {
    const fieldData = fields.find((field: any) => field.fieldName === fieldName);
    return fieldData ? fieldData.isDefault : false;
}

export const isGetDefaultValue = (fields: any[], fieldName: string) => {
    if (!fields || fields.length === 0) return ""; // Prevents errors when fields are undefined
    const field = fields.find((f) => f.fieldName === fieldName);
    return field && field.isDefault ? field.defaultValue || "" : "";
};

export const isGetFieldErrorMessage = (fields: any, fieldName: string): string | null => {
    const fieldData = fields.find((field: any) => field.fieldName.toLowerCase() === fieldName.toLowerCase());
    if (!fieldData) { return null; }
    return fieldData.otherError || null;
};

export const isGetFieldLengthErrorMessage = (fields: any, fieldName: string): string | null => {
    const fieldData = fields.find((field: any) => field.fieldName.toLowerCase() === fieldName.toLowerCase());
    if (!fieldData) { return null; }
    return fieldData.lengthError || null;
};

export const isGetFieldMinLength = (fields: any, fieldName: string) => {
    const fieldData = fields.find((field: any) => field.fieldName === fieldName);
    return fieldData ? fieldData.minLength : 1;
}

export const isGetFieldMaxLength = (fields: any, fieldName: string) => {
    const fieldData = fields.find((field: any) => field.fieldName === fieldName);
    return fieldData ? fieldData.maxLength : 100;
}