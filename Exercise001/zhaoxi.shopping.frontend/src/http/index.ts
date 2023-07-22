// 发起 HTTP 请求
import axios from "axios";
import { ElLoading, ElMessage } from "element-plus";
import ApiResult from "../class/ApiResult";
import store from "../store/index"; // 全局状态管理

// 创建拦截器对象
const instance = axios.create({
    // timeout: 6000
    headers: {
        "Content-Type": "application/json",
        // "Authorization": "Bearer ******"
    }
});

// 拦截请求
let loadingInstance: any; // 设置 Loading
instance.interceptors.request.use(config => {
    loadingInstance = ElLoading.service({
        lock: true,
        text: "Loading",
        background: "rgba(0,0,0,0.7)"
    });
    // 全局状态管理需要在这里获取，否则会提示没有初始化引用
    config.headers.Authorization = "Bearer " + store().Token;
    return config;
});

// 拦截响应
instance.interceptors.response.use(config => {
    loadingInstance.close();
    // 取得请求结果后统一返回，并设置返回结果
    const result: ApiResult = config.data;
    if (!result.IsSuccess) {
        ElMessage.error(result.Msg);
    }
    // return config;
    // 对于业务模块，只需关注结果，无需做验证提示
    return result.Result;
});

export const GetToken = (obj: {}) => {
    return instance.post("/api/Login/GetToken", obj);
};
export const GetNewToken = (userID: string) => {
    return axios.get(`/api/Login/RefreshToken?userID=${userID}`);
};

// 菜单模块
export const AddMenu = (req: {}) => {
    return instance.post("/api/Menu/Add", req);
};
export const EditMenu = (req: {}) => {
    return instance.post("/api/Menu/Edit", req);
};
export const DeleteMenu = (id: string) => {
    return instance.get(`/api/Menu/Delete?id=${id}`);
};
export const GetMenus = (req: {}) => {
    return instance.post("/api/Menu/GetMenus", req);
};

// 角色模块
export const AddRole = (req: {}) => {
    return instance.post("/api/Role/Add", req);
};
export const EditRole = (req: {}) => {
    return instance.post("/api/Role/Edit", req);
};
export const DeleteRole = (id: string) => {
    return instance.get(`/api/Role/Delete?id=${id}`);
};
export const GetRoles = (req: {}) => {
    return instance.post("/api/Role/GetRoles", req);
};
export const SettingMenu = (roleID: string, menuIDs: string) => {
    return instance.get(`/api/Menu/Delete?roleID=${roleID}&menuIDs=${menuIDs}`);
};