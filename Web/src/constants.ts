import axios from "axios";

export const BaseURL = "http://5e3fc5d8e9ea.ngrok.io/DocAnalyzerApi";

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