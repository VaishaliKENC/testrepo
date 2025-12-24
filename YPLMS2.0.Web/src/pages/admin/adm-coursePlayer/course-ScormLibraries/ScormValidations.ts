let decimalPointDelimiter = ".";
let defaultEmptyOK = false;

export class ScormValidations{

    fExtResValidate(lValidationType:string, strValue:any, lVocLookup:any, LValue:any, HValue:any):boolean {
        const StringValue = strValue.toString();
    
        switch (lValidationType.toUpperCase()) {
        case "CMIBLANK":
            if (StringValue.length > 0) {
                return false;
            }
            break;
        case "CMIBOOLEAN":
            if (!(StringValue == "true" || StringValue == "false")) {
                return false;
            }
            break;
        case "CMIDECIMAL":
            if (!this.isSignedFloat(StringValue)) {
                return false;
            }
            break;
        case "CMIIDENTIFIER":
    
            if (StringValue.length > 255) {
                return false;
            }
            if (!( /^[\w\d\.]+$/ .test(StringValue))) {
                return false;
            }
            break;
        case "CMIINTEGER":
            if (!(this.isInteger(StringValue))) {
                return false;
            }
    
            strValue = parseInt(strValue);
            if (strValue < 0 || strValue > 65536) {
                return false;
            }
            break;
        case "CMISINTEGER":
            if (!(this.isSignedInteger(StringValue))) {
                return false;
            }
    
            strValue = parseInt(strValue);
            if (strValue < -32768 || strValue > 32768 || strValue > parseInt(HValue) || strValue < parseInt(LValue)) {
                return false;
            }
            break;
        case "CMISTRING255":
            if (StringValue.length > 255) {
                return false;
            }
            break;
        case "CMISTRING4096":
            if (StringValue.length > 4096) {
                return false;
            }
            break;
        // case "CMITIME":
        //     if (!(this.fCheckCMITime(StringValue))) {
        //         return false;
        //     }
        //     break;
        // case "CMITIMESPAN":
        //     if (!(this.fCheckCMITimeSpan(StringValue))) {
        //         return false;
        //     }
        //     break;
        // case "CMIVOCABULARY":
        //     if (!this.fValidateLookup(StringValue, lVocLookup)) {
        //         return false;
        //     }
        //     break;
        }
        return true;
    }

    // fCheckCMITime(StringValue:string):boolean{
    //     var tmp=StringValue.split(':');
    //     if (tmp.length != 3 || tmp[0].length != 2 || tmp[1].length != 2){
    //        return false;
    //     }
    //     if (!(this.fCheckTimeSpan(StringValue, 24, false))){
    //        return false;
    //     }
        
    //     return true;
    //  }
     
    //  fCheckCMITimeSpan(StringValue:string):boolean{
    //     var tmp=StringValue.split(':');
    //     if (tmp.length != 3 || tmp[0].length < 2 || tmp[0].length > 4 || tmp[1].length != 2){
    //        return false;
    //     }
    //     if (!(this.fCheckTimeSpan(StringValue, 10000, true))){
    //        return false;
    //     }
        
    //     return true;
    //  }
     
    //  fCheckTimeSpan(StringValue:string, maxHrs:any, lbTimeSpan:boolean):boolean{
    //      let tmp = StringValue.split(':');
    //      let iMins;
    //      /********** valid hour **********************/
    //      let itm = parseInt(tmp[0]);
    //      if (isNaN(itm)) {
    //          return false;
    //      }
     
    //      /********** valid min *************************/
    //     //  if (itm >= 0 && itm < maxHrs) {
    //     //      itm = parseInt(tmp[1]);
    //     //      if (isNaN(itm)) {
    //     //          return false;
    //     //      }
    //     //      /******************** valid sec *****************/
    //     //      if (lbTimeSpan)
    //     //          iMins = 100; 						//to make scorm complient
    //     //      else
    //     //          iMins = 60;
    //     //      if (itm >= 0 && itm < iMins) {
    //     //          itm = tmp[2].indexOf('.');
    //     //          if (itm == -1) {
    //     //              itm = parseInt(tmp[2]);
    //     //              if (isNaN(tmp[2])) {
    //     //                  return false;
    //     //              }
    //     //              if (itm >= 0 && itm < 60)
    //     //                  return true;
    //     //          }
    //     //          else {
    //     //              if (itm == 2) {
    //     //                  tmp = tmp[2].split('.');
    //     //                  {
    //     //                      itm = parseInt(tmp[0]);
    //     //                      if (isNaN(tmp[0])) {
    //     //                          return false;
    //     //                      }
    //     //                      if (itm >= 0 && itm < 60) {
    //     //                          if (tmp[1].length > 2) {
    //     //                              return false;
    //     //                          }
    //     //                          itm = parseInt(tmp[1]);
    //     //                          if (isNaN(tmp[1])) {
    //     //                              return false;
    //     //                          }
    //     //                          if (itm >= 0 && itm <= 99)
    //     //                              return true;
    //     //                          else {
    //     //                              return false;
    //     //                          }
    //     //                      }
    //     //                  }
    //     //              }
    //     //          }
    //     //      }
    //     //  }
    //     //  return false;
    //  }

    //  fValidateLookup(lVal:string, lLookup:string):boolean {
    //     let sRetVal = gScormLookups[lLookup + "__" + lVal];
    //     if (!this.fIsUndefined(sRetVal)) {
    //         return true;
    //     }
    
    //     sRetVal = gScormLookups[lLookup + "__CheckFor"];
    //     if (!this.fIsUndefined(sRetVal)) {
    //         if (this.fExtResValidate(sRetVal, lVal, "", "", "")) {
    //             return true;
    //         }
    //     }
    
    //     return false;
    // }

    isInteger (s:any){
        let i;
    
        if (this.isEmpty(s))
           if (this.isInteger.arguments.length == 1) return defaultEmptyOK;
           else return (this.isInteger.arguments[1] == true);
    
        for (i = 0; i < s.length; i++)
        {
            // Check that current character is number.
            var c = s.charAt(i);
    
            if (!this.isDigit(c)) return false;
        }
    
        // All characters are numbers.
        return true;
    }
    
    isEmpty(s:any){
        return ((s == null) || (s.length == 0));
    }
    
    isSignedInteger (s:any){
        if (this.isEmpty(s))
           if (this.isSignedInteger.arguments.length == 1) return defaultEmptyOK;
           else return (this.isSignedInteger.arguments[1] == true);
    
        else {
            let startPos = 0;
            let secondArg = defaultEmptyOK;
    
            if (this.isSignedInteger.arguments.length > 1)
                secondArg = this.isSignedInteger.arguments[1];
    
            // skip leading + or -
            if ( (s.charAt(0) == "-") || (s.charAt(0) == "+") )
               startPos = 1;
            //return (this.isInteger(s.substring(startPos, s.length), secondArg));
            return (this.isInteger(s.substring(startPos, s.length)));
        }
    }
    
    isDigit (c:any){
        return ((c >= "0") && (c <= "9"));
    }
    
    isFloat (s:any){
        var i;
        var seenDecimalPoint = false;
    
        if (this.isEmpty(s))
           if (this.isFloat.arguments.length == 1) return defaultEmptyOK;
           else return (this.isFloat.arguments[1] == true);
    
        if (s == decimalPointDelimiter) return false;
    
        // Search through string's characters one by one
        // until we find a non-numeric character.
        // When we do, return false; if we don't, return true.
    
        for (i = 0; i < s.length; i++)
        {
            // Check that current character is number.
            var c = s.charAt(i);
    
            if ((c == decimalPointDelimiter) && !seenDecimalPoint) seenDecimalPoint = true;
            else if (!this.isDigit(c)) return false;
        }
    
        // All characters are numbers.
        return true;
    }

    // isSignedFloat (s:any){
    //     if (this.isEmpty(s))
    //        if (this.isSignedFloat.arguments.length == 1) return defaultEmptyOK;
    //        else return (this.isSignedFloat.arguments[1] == true);
    
    //     else {
    //         let startPos = 0;
    //         let secondArg = defaultEmptyOK;
    
    //         if (this.isSignedFloat.arguments.length > 1)
    //             secondArg = this.isSignedFloat.arguments[1];
    
    //         // skip leading + or -
    //         if ( (s.charAt(0) == "-") || (s.charAt(0) == "+") )
    //            startPos = 1;
    //         //return (this.isFloat(s.substring(startPos, s.length), secondArg));
    //         return (this.isFloat(s.substring(startPos, s.length)));
    //     }
    // }

    isSignedFloat(s: string, allowEmpty: boolean = defaultEmptyOK): boolean {
        if (this.isEmpty(s)) {
          return allowEmpty;
        }
    
        let startPos = 0;
    
        // skip leading + or -
        if (s.charAt(0) === "-" || s.charAt(0) === "+") {
          startPos = 1;
        }
    
        return this.isFloat(s.substring(startPos));
      }

    fIsUndefined(val: any): boolean {
        return typeof val === 'undefined';
    }
}