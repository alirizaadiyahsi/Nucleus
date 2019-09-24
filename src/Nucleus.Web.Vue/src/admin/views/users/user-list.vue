<template>
    <div>
        <v-toolbar flat color="white">
            <v-toolbar-title>{{$t('Users')}}</v-toolbar-title>
            <v-divider class="mx-2"
                       inset
                       vertical>
            </v-divider>
            <v-spacer></v-spacer>
            <v-text-field v-model="search"
                          append-icon="mdi-magnify"
                          :label="$t('Search')"
                          single-line
                          hide-details>
            </v-text-field>
            <v-spacer></v-spacer>
            <v-btn v-if="nucleus.auth.isGranted('Permissions_User_Create')" @click="editUser()" color="primary" dark class="mb-2">{{$t('NewUser')}}</v-btn>
            <v-dialog v-model="dialog" max-width="500px">
                <v-card>
                    <v-card-title>
                        <span class="headline">{{ formTitle }}</span>
                    </v-card-title>

                    <v-card-text>
                        <div v-for="error in errors" :key="error.name">
                            <v-alert :value="true" type="error">
                                {{$t(error.name)}}
                            </v-alert>
                        </div>
                        <v-form ref="form" @keyup.native.enter="save">
                            <v-text-field name="userName" :label="$t('UserName')" type="text"
                                          v-model="createOrUpdateUserInput.user.userName"
                                          :rules="[requiredError]"></v-text-field>
                            <v-text-field name="email" :label="$t('EmailAddress')" type="text"
                                          v-model="createOrUpdateUserInput.user.email"
                                          :rules="[requiredError,emailError]"></v-text-field>
                            <v-text-field v-if="!isEdit" name="password" :label="$t('Password')" type="password"
                                          v-model="createOrUpdateUserInput.user.password"
                                          :rules="[requiredError]"></v-text-field>
                            <v-text-field v-if="isEdit" name="password" :label="$t('Password')" type="password"
                                          v-model="createOrUpdateUserInput.user.password"></v-text-field>
                            <v-text-field v-if="!isEdit" name="passwordRepeat" :label="$t('PasswordRepeat')" type="password"
                                          v-model="createOrUpdateUserInput.user.passwordRepeat"
                                          :error-messages='passwordMatchError(createOrUpdateUserInput.user.password,createOrUpdateUserInput.user.passwordRepeat)'
                                          :rules="[requiredError]"></v-text-field>
                            <v-text-field v-if="isEdit" name="passwordRepeat" :label="$t('PasswordRepeat')" type="password"
                                          v-model="createOrUpdateUserInput.user.passwordRepeat"
                                          :error-messages='passwordMatchError(createOrUpdateUserInput.user.password,createOrUpdateUserInput.user.passwordRepeat)'></v-text-field>
                            <v-list dense>
                                <v-list-item v-for="item in allRoles" :key="item.id">
                                    <v-checkbox v-model="createOrUpdateUserInput.grantedRoleIds" :label="item.name" :value="item.id"></v-checkbox>
                                </v-list-item>
                            </v-list>
                        </v-form>
                    </v-card-text>

                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" text @click="dialog = false">{{$t('Cancel')}}</v-btn>
                        <v-btn color="blue darken-1" text @click="save">{{$t('Save')}}</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-toolbar>

        <v-data-table :headers="headers"
                      :items="pagedListOfUserListDto.items"
                      :options.sync="options"
                      :server-items-length="pagedListOfUserListDto.totalCount"
                      :loading="loading"
                      class="elevation-1">
            <template v-slot:item.action="{ item }">
                <v-icon v-if="nucleus.auth.isGranted('Permissions_User_Update')"
                        small
                        class="mr-2"
                        @click="editUser(item.id)">
                    mdi-pencil
                </v-icon>
                <v-icon v-if="nucleus.auth.isGranted('Permissions_User_Delete')"
                        small
                        @click="deleteUser(item.id)">
                    mdi-delete
                </v-icon>
            </template>
            <template slot="no-data" v-if="!loading">
                <v-alert :value="true" color="error" icon="warning">
                    {{$t('NothingToDisplay')}}
                </v-alert>
            </template>
        </v-data-table>
    </div>
</template>

<script src="./user-list.ts"></script>