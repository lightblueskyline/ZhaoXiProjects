<template>
    <el-dialog v-model="dialogVisable" :title="form.Id != '' ? '编辑' : '新增'" width="30%" @close="$emit('closeAdd')">
        <el-form :model="form" label-width="80px" ref="ruleFormRef" :rules="rules">
            <el-form-item label="名称" prop="Name">
                <el-input v-model="form.Name" />
            </el-form-item>
            <el-form-item label="昵称" prop="NickName">
                <el-input v-model="form.NickName" />
            </el-form-item>
            <el-form-item label="密码" prop="Password">
                <el-input v-model="form.Password" />
            </el-form-item>
            <el-form-item label="是否启用" prop="IsEnable">
                <el-switch v-model="form.IsEnable" />
            </el-form-item>
            <el-form-item label="描述" prop="Description">
                <el-input v-model="form.Description" />
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="closeAdd(ruleFormRef)">取消</el-button>
                <el-button type="primary" @click="save(ruleFormRef)">提交</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<script setup lang="ts">
import { computed, reactive, ref } from "vue";
import UserResponse from "../../../class/UserResponse";
import { FormInstance, FormRules } from "element-plus";
import { AddUser, EditUser } from "../../../http";
// --- 变量 ---
const props = defineProps({
    IsShow: Boolean,
    Info: UserResponse
});
const dialogVisable = computed(() => props.IsShow);
const form = ref({
    Id: "",
    Name: "",
    NickName: "",
    Password: "",
    // Order: 0,
    IsEnable: false,
    Description: "",
});
const ruleFormRef = ref<FormInstance>();
const emits = defineEmits(["closeAdd", "success"]);
const rules = reactive<FormRules>({
    Name: [
        { required: true, message: '请输入名称', trigger: 'blur' }
    ],
    Password: [
        { required: true, message: '请输入密码', trigger: 'blur' }
    ],
    Order: [
        { required: true, message: '请输入一个序号' },
        { type: 'number', message: '该字段必须是数字' }
    ]
})
// --- 方法 ---
const closeAdd = async (formEl: FormInstance | undefined) => {
    if (!formEl) {
        return;
    }
    formEl.resetFields();
    emits("closeAdd");
};
const save = async (formEl: FormInstance | undefined) => {
    if (!formEl) {
        return;
    }
    await formEl.validate((valid, fields) => {
        if (valid) {
            if (form.value.Id) {
                EditUser(form.value).then(function (response) {
                    if (response) {
                        emits("success", "编辑成功！")
                    }
                });
            } else {
                AddUser(form.value).then(function (response) {
                    if (response) {
                        emits("success", "新增成功！")
                    }
                });
            }
        } else {
            console.log("提交异常", fields);
        }
    });
};
</script>