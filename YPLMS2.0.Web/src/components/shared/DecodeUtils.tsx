  import CryptoJS  from 'crypto-js'
 
  // Handle Encryption 
  export const SecureEncrypt = (userPassword: string): string => {
    {
       // Encrypt using AES (similar to DES, but more secure)
     const yM_key = CryptoJS.enc.Utf8.parse([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24].map(b => String.fromCharCode(b)).join(''));
     const yM_iv = CryptoJS.enc.Utf8.parse([8, 7, 6, 5, 4, 3, 2, 1].map(b => String.fromCharCode(b)).join(''));
   
     const encrypted = CryptoJS.TripleDES.encrypt(CryptoJS.enc.Utf8.parse(userPassword), yM_key, {
                       iv: yM_iv,
                       mode: CryptoJS.mode.CBC,
                       padding: CryptoJS.pad.Pkcs7
                       });
   
     return CryptoJS.enc.Base64.stringify(encrypted.ciphertext); // Return Base64-encoded string
    };
   }


 // Handle Decryption 
   export const Decryption = (encryptedText: string): string => {
       const yM_key = CryptoJS.enc.Utf8.parse(
           [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24]
               .map(b => String.fromCharCode(b))
               .join('')
       );
       
       const yM_iv = CryptoJS.enc.Utf8.parse(
           [8, 7, 6, 5, 4, 3, 2, 1]
               .map(b => String.fromCharCode(b))
               .join('')
       );
   
       const decrypted = CryptoJS.TripleDES.decrypt(
        encryptedText,  // Pass the base64-encoded string directly
        yM_key,
        {
            iv: yM_iv,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7,
        }
    );
       return decrypted.toString(CryptoJS.enc.Utf8);
   };
   
   // Usage Example
  //  console.log(Decryption("B184ta9Bo6CYk/hj4PoZvg=="));
   
   // export const generateRandomString = (length: number): string => {
//     // Define the set of characters from which you want to generate random characters
//     const characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
//     let randomString = "";
  
//     for (let i = 0; i < length; i++) {
//       // Generate a random index within the range of characters
//       const randomIndex = Math.floor(Math.random() * characters.length);
//       // Get the character at the random index
//       randomString += characters.charAt(randomIndex);
//     }
  
//     return "+" + randomString;
//   };
  
  // export const decryptData = (base64EncodedString: string): string => {
  //   if (base64EncodedString.length < 6) {
  //     throw new Error("Input string is too short to be decrypted properly.");
  //   }
    
  //   // Get the first 6 characters of the input string
  //   const firstPart: string = base64EncodedString.slice(0, 6);
  
  //   // Get the rest of the input string starting from the 7th character
  //   const secondPart: string = base64EncodedString.slice(6);
  
  //   // Decode the second part of the base64-encoded string using the atob function
  //   return atob(secondPart);
  // };