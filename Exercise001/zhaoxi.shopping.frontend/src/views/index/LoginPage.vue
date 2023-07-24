<template>
    <div class="main">
        <el-form class="form" :model="form" :rules="rules" ref="loginForm" label-width="120px">
            <el-form-item label="用户名" prop="userName">
                <el-input v-model="form.userName" />
            </el-form-item>
            <el-form-item label="密码" prop="passWord">
                <el-input v-model="form.passWord" type="password" @keyup.enter="onSubmit(loginForm)" />
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="onSubmit(loginForm)">提交</el-button>
                <el-button @click="reset(loginForm)">重置</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue';
import { ElMessage, FormInstance, FormRules } from 'element-plus';
import router from '../../router'; // 导入路由
import store from "../../store/index";
import { GetToken } from "../../http/index";

const form = reactive({
    userName: "",
    passWord: ""
});

const loginForm = ref<FormInstance>();

// 表单验证规则
const rules = reactive<FormRules>({
    userName: [{
        required: true,
        message: "请输入用户名",
        trigger: "blur"
    }],
    passWord: [{
        required: true,
        message: "请输入密码",
        trigger: "blur"
    }]
});

const onSubmit = async (loginForm: FormInstance | undefined) => {
    // 联合类型 -> loginForm: FormInstance | undefined
    if (!loginForm) {
        // undefined 时返回
        return;
    }
    // 验证表单数据
    await loginForm.validate(async (valid: boolean, fields: any) => {
        if (valid) {
            // 请求登录接口
            let token: string = await GetToken(form) as any as string;
            if (token != null) {
                ElMessage.success("验证成功！");
                store().$patch((state) => {
                    state.Token = token;
                    state.RefreshTokenCount = 0 // 每次重新登录之后重置无感率先你 Token 次数
                });
                router.push({
                    path: "/desktop"
                });
            }
        } else {
            ElMessage.error("验证失败！");
            console.log(fields); // 打印验证失败列
        }
    });
};

const reset = (loginForm: FormInstance | undefined) => {
    if (!loginForm) {
        // undefined 时返回
        return;
    }
    loginForm.resetFields();
};
</script>

<style lang="scss" scoped>
.main {
    background-image: url('/bg.png');
    height: 60vh;
    padding-top: 20%;
}

.form {
    width: 30%;
    margin: 0px auto;
}
</style>