// 请求 WebAPI
import axios from "axios"; // 导入包
import userStore from "../store/index";
import ApiResult from "../class/ApiResult";
import router from "../router/index";
import { ElMessage } from "element-plus";

// 创建实例
const instance = axios.create({
    timeout: 3000,
    headers: {
        "Content-Type": "application/json",
        // 这里无法使用 pinia
        // "Authorization": "Bearer "+"Your Token"
    }
});

// 拦截请求
instance.interceptors.request.use(
    config => {
        // 全局状态管理需要在这里获取，否则会提示没有初始化引用
        config.headers.Authorization = "Bearer " + userStore().token;
        return config;
    }
);

// 拦截响应
instance.interceptors.response.use(
    response => {
        // 拿到请求结果后统一返回，并设置返回结果
        const tempResponse: ApiResult = response.data;
        if (!tempResponse.IsSuccess) {
            ElMessage.error(tempResponse.Msg);
        }
        // 对于业务模块，只需关注结果，无需做验证提示
        return tempResponse.Result;
    },
    // 请求异常走这里
    error => {
        // 请求的配置对象
        const originalRequest = error.config;
        // undefined
        if (!originalRequest) {
            return Promise.reject(error);
        }
        // 根据不同的状态码，做出不同的响应
        if (error.response.status === 401) {
            // 401 表示未授权 -> 登录页
            router.push({
                path: "/login"
            });
        } else if (error.response.status === 500) {
            ElMessage.error("内部服务器错误，请检查服务是否启动！");
        } else if (error.response.status === 404) {
            ElMessage.error("接口地址不存在，请检查接口地址！");
        } else if ((JSON.stringify(error)).indexOf("time out") > 1) {
            ElMessage.error("请求接口超时！");
        }
        // 请求失败返回错误信息
        return Promise.reject(error);
    }
);

export const GetToken = (obj: {}) => {
    // return axios.post("http://localhost:5271/api/Login/GetToken");
    return axios.post(`/api/Login/GetToken`, obj);
};

// export const GetMenus = () => { };