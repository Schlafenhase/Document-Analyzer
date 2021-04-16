import axios from "axios";

export const BaseURL = "http://373c540f26a0.ngrok.io/DocAnalyzerApi";

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
