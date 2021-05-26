import axios from "axios";

// export const BaseURL = "http://35ef82d08f1e.ngrok.io/DocAnalyzerApi";
export const BaseURL = "https://localhost:44328";
export const AuthURL = "http://localhost:8081";

export const checkToken =  async () => {
    const savedToken = localStorage.getItem("token");
    if (savedToken) {
      const response = await axios.get(BaseURL + "/Api/Employee/Employees", {
        headers: {
          Authorization: "Bearer " + savedToken,
        },
      });
      if (response.data) {
        console.log();
        return savedToken
      }
    }
    return ""
  };
