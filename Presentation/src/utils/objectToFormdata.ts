export function objectToFormData(obj: Record<string, any>): FormData {
  const formData = new FormData();
  
  for (const key in obj) {
    const value = obj[key];
    
    if (value instanceof File || value instanceof Blob) {
      formData.append(key, value);
    } 

    else if (value instanceof FileList) {
      Array.from(value).forEach(file => {
        formData.append(key, file);
      });
    }

    else if (value !== undefined && value !== null) {
      formData.append(key, typeof value === 'object' 
        ? JSON.stringify(value) 
        : String(value)
      );
    }
  }
  
  return formData;
}