<script setup lang="ts">
import { ref } from 'vue';
import type { FormInstance } from "element-plus";

const value = ref(new Date());
const isDialogVisible = ref(false);
const form = ref({
    vDate: "",
    vToDo: ""
});
const formRef = ref<FormInstance>();
const listToDo = ref([
    {
        vDate: "2023-08-01",
        vToDo: "庆祝八一建军节"
    },
    {
        vDate: "2023-08-02",
        vToDo: "待办事项"
    }
]);
const readToDo = (pDate: string): string => {
    return listToDo.value.find(x => x.vDate == pDate)?.vToDo!;
};
// 显示弹出对话框，并设置其中 Form 表单值
const settingToDo = (pDate: string) => {
    form.value.vDate = pDate;
    // 查找当前日期是否存在值
    form.value.vToDo = readToDo(pDate);
    isDialogVisible.value = true;
};
// 取消显示弹出对话框
const dialogCancel = (pFormRef: FormInstance | undefined) => {
    isDialogVisible.value = false;
    pFormRef?.resetFields();
};
// 提交弹出对话框表单值
const dialogSubmit = (pFormRef: FormInstance | undefined) => {
    // 判断当前是否存在此待办项目
    let todo = readToDo(form.value.vDate);
    // 不存在，添加；存在，更新
    if (!todo) {
        listToDo.value.push({
            vDate: form.value.vDate,
            vToDo: form.value.vToDo
        });
    } else {
        listToDo.value.forEach(x => {
            if (x.vDate == form.value.vDate) {
                x.vToDo = form.value.vToDo;
            }
        });
    }
    isDialogVisible.value = false;
    pFormRef?.resetFields();
};
</script>

<template>
    <el-calendar v-model="value">
        <template #date-cell="{ data }">
            <div style="width: 100%;height: 100%;" @click="settingToDo(data.day)">
                <div>{{ data.day }}</div>
                <div>{{ readToDo(data.day) }}</div>
            </div>
        </template>
    </el-calendar>
    <el-dialog v-model="isDialogVisible" title="添加待办事项" width="30%">
        <el-form :model="form" ref="formRef">
            <el-form-item label="待办事项" label-width="90px">
                <el-input v-model="form.vToDo" autocomplete="off"></el-input>
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="dialogCancel(formRef)">Cancel</el-button>
                <el-button type="primary" @click="dialogSubmit(formRef)">
                    Confirm
                </el-button>
            </span>
        </template>
    </el-dialog>
</template>

<style scoped lang="scss"></style>