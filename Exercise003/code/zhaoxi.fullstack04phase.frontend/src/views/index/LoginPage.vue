<script setup lang="ts">
// reactive 定义复杂对象
import { ref, reactive } from "vue";
// FormInstance 获取表单实例
// FormRules 表单验证规则
import { ElMessage, type FormInstance, type FormRules } from "element-plus";
import { useRouter } from "vue-router"; // 导入路由
import { GetToken } from "../../http/index";
import userStore from "../../store/index";

const router = useRouter();

const url = ref("/images/logo.0606fdd2.png");
const boxbg = ref("/images/svgs/login-box-bg.svg");

const form = reactive({
    userName: "Admin",
    passWord: ""
});

const ruleFormRef = ref<FormInstance>(); // 表单验证对象和表单同名的变量
const rules = reactive<FormRules>({
    userName: [{ required: true, message: "请输入用户名", trigger: "blur" }],
    passWord: [{ required: true, message: "请输入密码", trigger: "blur", type: "number" }]
}); // 表单验证规则

const onSubmit = async (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) {
        return;
    }
    await ruleFormRef.validate(async (valid, fields) => {
        console.log(fields);
        if (valid) {
            // 请求登录接口
            let tempResponse = await GetToken(form) as any;
            if (tempResponse.status === 200) { // 请求成功
                if (tempResponse.data.IsSuccess) { // 合法用户
                    let tempToken: string = tempResponse.data.Result as string;
                    console.log(tempToken);
                    // 更新全局状态管理中的 Token
                    userStore().$patch({
                        token: tempToken
                    });
                    // 验证通过
                    ElMessage.success("验证通过，登录成功！");
                    // 跳转路由
                    router.push({
                        path: "/"
                    });
                }
            }
        } else {
            ElMessage.error("验证失败！");
        }
    });
};
</script>

<template>
    <div class="login">
        <div class="relative">
            <div class="left">
                <el-row>
                    <el-col :span="24">
                        <div class="homepageLogo">
                            <ul>
                                <li><el-image style="width: 50px; height: 50px" :src="url" /></li>
                                <li><span>M_0v0_M</span></li>
                            </ul>
                        </div>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-image class="boxbg" :src="boxbg" />
                        <p class="p1">欢迎使用本系统</p>
                        <p class="p2">西安凌安电脑有限公司</p>
                    </el-col>
                </el-row>
            </div>
            <div class="right">
                <el-row>
                    <el-col :span="24">
                        <h2>登录</h2>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <!-- :model 模型绑定 -->
                        <!-- :rules 表单验证规则 -->
                        <!-- ref="ruleFormRef" 表单别名，只要表单自己可以取得表单验证规则，所以使用表单别名。体现在模板引用，用以操作 DOM -->
                        <el-form :model="form" label-width="120px" label-position="top" size="large" class="form"
                            :rules="rules" ref="ruleFormRef">
                            <el-form-item label="用户名" prop="userName">
                                <el-input v-model="form.userName" />
                            </el-form-item>
                            <el-form-item label="密码" prop="passWord">
                                <el-input v-model.number="form.passWord" type="password" show-password
                                    @keyup.enter="onSubmit(ruleFormRef)" />
                            </el-form-item>
                            <el-form-item>
                                <el-button class="submitBtn" type="primary" @click="onSubmit(ruleFormRef)">登录</el-button>
                            </el-form-item>
                        </el-form>
                    </el-col>
                </el-row>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
.login {
    width: 100%;
    height: 100%;

    .relative {
        width: 100%;
        height: 100%;
        text-align: center;

        .left {
            width: 50%;
            height: 100%;
            float: left;
            background-image: url('/images/svgs/login-bg.svg');

            .boxbg {
                width: 350px;
                height: 350px;
                margin-top: 100px;
            }

            .homepageLogo {
                height: 50px;
                line-height: 50px;
                margin-top: 40px;

                span {
                    color: white;
                    font-size: 24px;
                }

                ul {
                    list-style: none;

                    li {
                        float: left;
                        margin-left: 5px;
                    }
                }
            }

            p {
                color: white;
            }

            .p1 {
                font-size: 1.875rem;
                line-height: 2.25rem;
            }

            .p2 {
                font-size: 0.875rem;
                line-height: 1.25rem;
            }
        }

        .right {
            width: 50%;
            float: left;
            padding-top: 15%;

            .form {
                width: 50%;
                margin: 0px auto;

                .submitBtn {
                    width: 100%;
                }
            }
        }
    }
}
</style>
