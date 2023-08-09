// 请求 WebAPI
import axios from "axios"; // 导入包
import userStore from "../store/index";
import ApiResult from "../class/ApiResult";
import router from "../router/index";
import { ElMessage } from "element-plus";
import { FormatToken } from "../tool";

// 创建实例
const instance = axios.create({
    timeout: 6000,
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
    async error => {
        // 请求的配置对象
        const originalRequest = error.config;
        // undefined
        if (!originalRequest) {
            return Promise.reject(error);
        }
        // 根据不同的状态码，做出不同的响应
        // _retry 表示是否应该重试
        if (error.response.status === 401 && !originalRequest._retry &&
            userStore().RefreshTokenCount < 3 &&
            userStore().token && userStore().token != "") {
            // 无感刷新机制
            // 每次重试时设置为 True
            originalRequest._retry = true;
            // 请求刷新 Token
            const newToken = (await RefreshToken(FormatToken(userStore().token)?.Id!)).data.Result as string;
            if (newToken) {
                // 拿到 Token 之后更新全局状态，设置下次请求的 Token 返回实例
                let tempNum = userStore().RefreshTokenCount + 1;
                userStore().$patch({
                    token: newToken,
                    RefreshTokenCount: tempNum
                });
                // 更新请求头部
                instance.defaults.headers.common["Authorization"] = `Bearer ${newToken}`;
                return instance(originalRequest);
            }
        } else if (error.response.status === 401) {
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

/**
 * 获取 Token
 * @param obj 
 * @returns 
 */
export const GetToken = (obj: {}) => {
    // return axios.post("http://localhost:5271/api/Login/GetToken");
    return axios.post(`/api/Login/GetToken`, obj);
};

/**
 * 刷新 Token
 * @param userId 用户ID
 * @returns 
 */
export const RefreshToken = (userId: string) => {
    return axios.get(`/api/Login/RefreshToken?userId=${userId}`);
};

// ------ 菜单模块 START ------
export const AddMenu = (obj: {}) => {
    return instance.post(`/api/Menu/AddMenu`, obj);
};

export const BatchDeleteMenu = (ids: string) => {
    return instance.get(`/api/Menu/BatchDeleteMenu?ids=${ids}`);
};

export const DeleteMenu = (id: string) => {
    return instance.get(`/api/Menu/DeleteMenu?id=${id}`);
};

export const EditMenu = (obj: {}) => {
    return instance.post(`/api/Menu/EditMenu`, obj);
};

export const GetMenus = (obj: {}) => {
    return instance.post(`/api/Menu/GetMenus`, obj);
};

export const SettingMenu = (roleId: string, menuIds: string) => {
    return instance.get(`/api/Menu/SettingMenu?roleId=${roleId}&menuIds${menuIds}`);
};
// ------ 菜单模块 END ------

// ------ 角色模块 START ------
export const AddRole = (obj: {}) => {
    return instance.post(`/api/Role/AddRole`, obj);
};

export const DeleteRole = (id: string) => {
    return instance.get(`/api/Role/DeleteRole?id=${id}`);
};

export const EditRole = (obj: {}) => {
    return instance.post(`/api/Role/EditRole`, obj);
};

export const GetRoles = (obj: {}) => {
    return instance.post(`/api/Role/GetRoles`, obj);
};
// ------ 角色模块 END ------

// ------ 用户模块 START ------
export const AddUser = (obj: {}) => {
    return instance.post(`/api/User/AddUser`, obj);
};

export const DeleteUser = (id: string) => {
    return instance.get(`/api/User/DeleteUser?id=${id}`);
};

export const EditNickNameOrPassword = (obj: {}) => {
    return instance.post(`/api/User/EditNickNameOrPassword`, obj);
};

export const EditUser = (obj: {}) => {
    return instance.post(`/api/User/EditUser`, obj);
};

export const GetUsers = (obj: {}) => {
    return instance.post(`/api/User/GetUsers`, obj);
};

export const SettingUserRole = (userId: string, roleIds: string) => {
    return instance.get(`/api/User/SettingUserRole?userId=${userId}&roleIds=${roleIds}`);
};
// ------ 用户模块 END ------