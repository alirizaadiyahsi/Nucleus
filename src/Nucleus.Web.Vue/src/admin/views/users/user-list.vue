<template>
    <div>
        <v-toolbar flat color="white">
            <v-toolbar-title>Users</v-toolbar-title>
            <v-divider class="mx-2"
                       inset
                       vertical>
            </v-divider>
            <v-spacer></v-spacer>
            <v-text-field v-model="search"
                          append-icon="search"
                          label="Search"
                          single-line
                          hide-details>
            </v-text-field>
            <v-spacer></v-spacer>
            <v-btn @click="editUser()" color="primary" dark class="mb-2">Create User</v-btn>
            <v-dialog v-model="dialog" max-width="500px">
                <v-card>
                    <v-card-title>
                        <span class="headline">{{ formTitle }}</span>
                    </v-card-title>

                    <v-card-text>
                        <div v-for="error in errors" :key="error.name">
                            <v-alert :value="true" type="error">
                                {{error.value}}
                            </v-alert>
                        </div>
                        <v-form ref="form">
                            <v-text-field name="userName" label="User name" type="text" v-model="createOrUpdateUserInput.user.userName" :rules="[appConsts.validationRules.required]"></v-text-field>
                            <v-text-field name="email" label="E-mail address" type="text" v-model="createOrUpdateUserInput.user.email" :rules="[appConsts.validationRules.required,appConsts.validationRules.email]"></v-text-field>
                            <v-text-field v-if="!isEdit" name="password" label="Password" type="password" 
                                          v-model="createOrUpdateUserInput.user.password" 
                                          :rules="[appConsts.validationRules.required]"></v-text-field>
                            <v-text-field v-if="isEdit" name="password" label="Password" type="password" 
                                          v-model="createOrUpdateUserInput.user.password"></v-text-field>
                            <v-text-field v-if="!isEdit" name="passwordRepeat" label="Repeat password" type="password"
                                          v-model="createOrUpdateUserInput.user.passwordRepeat" 
                                          :error-messages='passwordMatchError(createOrUpdateUserInput.user.password,createOrUpdateUserInput.user.passwordRepeat)' 
                                          :rules="[appConsts.validationRules.required]"></v-text-field>
                            <v-text-field v-if="isEdit" name="passwordRepeat" label="Repeat password" type="password"
                                          v-model="createOrUpdateUserInput.user.passwordRepeat" 
                                          :error-messages='passwordMatchError(createOrUpdateUserInput.user.password,createOrUpdateUserInput.user.passwordRepeat)'></v-text-field>
                            <v-list dense subheader>
                                <v-subheader>Select Roles</v-subheader>
                                <v-list-tile v-for="item in allRoles" :key="item.id">
                                    <v-list-tile-content>
                                        <v-checkbox v-model="createOrUpdateUserInput.grantedRoleIds" :label="item.name" :value="item.id"></v-checkbox>
                                    </v-list-tile-content>
                                </v-list-tile>
                            </v-list>
                        </v-form>
                    </v-card-text>

                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" flat @click="dialog = false">Cancel</v-btn>
                        <v-btn color="blue darken-1" flat @click="save">Save</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-toolbar>

        <v-data-table :headers="headers"
                      :items="pagedListOfUserListDto.items"
                      :pagination.sync="pagination"
                      :total-items="pagedListOfUserListDto.totalCount"
                      :loading="loading"
                      class="elevation-1">
            <template slot="items" slot-scope="props">
                <td>{{ props.item.userName }}</td>
                <td>{{ props.item.email }}</td>
                <td class="justify-center layout px-0">
                    <v-icon v-if="!isAdminUser(props.item.userName)" small
                            class="mr-2"
                            @click="editUser(props.item.id)">
                        edit
                    </v-icon>
                    <v-icon v-if="!isAdminUser(props.item.userName)" small
                            @click="deleteUser(props.item.id)">
                        delete
                    </v-icon>
                </td>
            </template>
            <template slot="no-data" v-if="!loading">
                <v-alert :value="true" color="error" icon="warning">
                    Sorry, nothing to display here :(
                </v-alert>
            </template>
        </v-data-table>
    </div>
</template>

<script src="./user-list.ts"></script>
