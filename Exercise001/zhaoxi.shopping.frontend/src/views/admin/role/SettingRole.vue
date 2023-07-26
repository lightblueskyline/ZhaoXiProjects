<template>
    <el-dialog v-model="dialogVisible" title="设置菜单" style="width:30vw;height: 50vh;" draggable
        @close="$emit('closeSettingAdd')" @opened="opened">
        <div>
            <el-tree-select ref="tree" style="width: 90%;" v-model="value" :data="store().UserMenus" :props="{
                children: 'Children',
                label: 'Name',
                value: 'Id'
            }" node-key="Id" :default-checked-keys="selectArr" :render-after-expand="false" show-checkbox multiple />
        </div>
        <template #footer>
            <span class="dialog-footer" style="position: absolute;bottom: 20px;left: 0px;">
                <el-button @click="close()">取消</el-button>
                <el-button type="primary" @click="save()">确定</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<script setup lang="ts">
import { ref, computed } from "vue";
import store from "../../../store/index";
import { SettingMenu } from "../../../http/index";
import { ElTree } from "element-plus";

const props = defineProps({
    isShow: Boolean,
    roleID: String
});
const dialogVisible = computed(() => props.isShow);
const emits = defineEmits(["closeSettingRole", "settingRoleSuccess"]);
const value = ref();
const close = () => {
    emits("closeSettingRole");
};
const tree = ref<InstanceType<typeof ElTree>>();
const save = async () => {
    // 获取当前选择的树节点，该函数会返回子节点喝父节点
    console.log(tree.value!.getCheckedNodes(false, true));
    let menuIDs = tree.value!.getCheckedNodes(false, true).map((item: any) => item.ID).join(",");
    console.log(menuIDs);
    let res = (await SettingMenu(props.roleID!, menuIDs) as any) as boolean;
    if (res) {
        emits("settingRoleSuccess");
    }
};
const selectArr = ref<string[]>([]);
// PS: 需要在 Dialog 的打开动画结束时的回调函数中赋值 tree-selected
// 否则 onMounted 里无法获取到组件的实例，因为页面加载的时候 dom 是不显示的
const opened = () => {
    selectArr.value = ["******"];
};
</script>

<style scoped lang="scss"></style>