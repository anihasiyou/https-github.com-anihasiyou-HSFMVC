using DapperExtensions;
using System;

namespace FineAdmin.Model
{
    ///<summary>
    ///
    ///</summary>
    [Table(TableName="tb_employee", KeyName= "employee_id")]
    public partial class tb_employeeModel
    {
           /// <summary>
           /// Desc:员工编码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string employee_id {get;set;}

           /// <summary>
           /// Desc:名字
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string name {get;set;}

           /// <summary>
           /// Desc:员工编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string code {get;set;}

           /// <summary>
           /// Desc:性别 0：男 1：女
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int sex {get;set;}

           /// <summary>
           /// Desc:所属的权限组
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int power_group {get;set;}

           /// <summary>
           /// Desc:职位
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string job {get;set;}

           /// <summary>
           /// Desc:密码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string password {get;set;}

           /// <summary>
           /// Desc:是否有效
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int work_flag {get;set;}

           /// <summary>
           /// Desc:电子邮件
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string email {get;set;}

           /// <summary>
           /// Desc:电子邮件的帐户名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string emailaccount {get;set;}

           /// <summary>
           /// Desc:电子邮件的密码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string emailpwd {get;set;}

           /// <summary>
           /// Desc:部门
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string department {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string departmentGroup {get;set;}

           /// <summary>
           /// Desc:生日
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string birthday {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string remark {get;set;}

           /// <summary>
           /// Desc:身份证
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string IDC {get;set;}

           /// <summary>
           /// Desc:家庭住址
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string address {get;set;}

           /// <summary>
           /// Desc:联系电话
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string tel {get;set;}

           /// <summary>
           /// Desc:部门编码
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int depart_id {get;set;}

           /// <summary>
           /// Desc:职位编码
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int position_id {get;set;}

           /// <summary>
           /// Desc:角色组编码
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int act_id {get;set;}

           /// <summary>
           /// Desc:角色编码
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int role_id {get;set;}

           /// <summary>
           /// Desc:所属公司
           /// Default:北京泽坤
           /// Nullable:False
           /// </summary>           
           public string company {get;set;}

           /// <summary>
           /// Desc:上级主管
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string manager {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string vbusr {get;set;}

           /// <summary>
           /// Desc:分机号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ext {get;set;}

    }
}
