<template>
    <el-dialog v-model="dialogVisible" :title="form.Id != '' ? '编辑' : '新增'" width="30%" @close="$emit('closeAddEdit')">
        <el-form :model="form" label-width="80px" ref="ruleFormRef" :rules="formRules">
            <el-form-item label="名称" prop="Name">
                <el-input v-model="form.Name"></el-input>
            </el-form-item>
            <el-form-item label="排序" prop="Order">
                <el-input v-model="form.Order"></el-input>
            </el-form-item>
            <el-form-item label="是否启用" prop="IsEnable">
                <el-switch v-model="form.IsEnable"></el-switch>
            </el-form-item>
            <el-form-item label="描述" prop="Description">
                <el-input v-model="form.Description"></el-input>
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="closeAddEdit(ruleFormRef)">取消</el-button>
                <el-button type="primary" @click="save(ruleFormRef)">提交</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<script setup lang="ts">
import { computed, reactive, ref, watch } from "vue";
import RoleModel from "../../../class/RoleModel";
import { ElMessage, FormInstance, FormRules } from "element-plus";
import { AddRole, EditRole } from "../../../http";

// --- 变量 ---
const props = defineProps({
    isShowAddEdit: Boolean,
    modelAddEdit: RoleModel
});
const dialogVisible = computed(() => props.isShowAddEdit);
const ruleFormRef = ref<FormInstance>();
const form = ref({
    Id: "",
    Name: "",
    Order: 0,
    IsEnable: false,
    Description: ""
});
const formRules = reactive<FormRules>({
    Name: [{ required: true, message: "请输入名称", trigger: "blur" }],
    Order: [{ required: true, message: "请输入序号" }, { type: "number", message: "该字段必须为数字" }]
});
const emits = defineEmits(["closeAddEdit", "successAddEdit"]);

// --- 方法 ---
// 组件的实例只会在加载的时候渲染一次，若要实现 Form 的值和参数联动，需要使用监听
watch(() => props.modelAddEdit, (newInfo: any) => {
    if (newInfo) {
        form.value = newInfo;
    }
});
const closeAddEdit = (formEl: FormInstance | undefined) => {
    if (!formEl) {
        return;
    }
    formEl.resetFields();
    emits("closeAddEdit");
};
const save = async (formEl: FormInstance | undefined) => {
    if (!formEl) {
        return;
    }
    await formEl.validate((valid, _fields) => {
        if (valid) {
            if (form.value.Id) {
                EditRole(form.value).then(function (response) {
                    if (response) {
                        emits("successAddEdit", "编辑成功！");
                    }
                });
            } else {
                AddRole(form.value).then(function (response) {
                    if (response) {
                        emits("successAddEdit", "新增成功！");
                    }
                });
            }
        } else {
            ElMessage.error("提交表单字段验证异常");
        }
    });
};
</script>

<style scoped lang="scss"></style>